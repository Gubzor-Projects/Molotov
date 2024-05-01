using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Mod.Resources;
namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static string ModTag = " [Gubzor][RB]";
        public static void Main()
        {
            Resources.LoadResources();
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Brick"),
                    NameOverride = "Molotov" + ModTag,
                    NameToOrderByOverride = "Radioactive",
                    DescriptionOverride = "Kabie!",
                    CategoryOverride = ModAPI.FindCategory("Explosives"),
                    ThumbnailOverride = ModAPI.LoadSprite("MolThumb.png", 5f),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Mol.png");
                        Instance.FixColliders();

                        Instance.GetComponent<PhysicalBehaviour>().Properties = ModAPI.FindPhysicalProperties("Metal");

                        Instance.GetOrAddComponent<RadioactiveBarrelBehaviour>();

                        if (!Instance.GetComponent<FirstSpawn>())
                        {
                            Instance.AddComponent<FirstSpawn>();
                        }
                    }
                }
            );
        }
    }
    public class RadioactiveBarrelBehaviour : MonoBehaviour
    {
        public float MaxDamage = 50f;
        public float Damage;
        public float MinimumImpactForce = 45f;
        private static readonly ContactPoint2D[] buffer = new ContactPoint2D[8];

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
        }
        private void EvaluateCollision(Collision2D collision)
        {
            int contacts = collision.GetContacts(buffer);
            if (Utils.GetMinImpulse(buffer, contacts) < MinimumImpactForce)
            {
                return;
            }
            Explode();
        }

        public void Shot(Shot shot)
        {
            Damage += shot.damage;
            if (Damage < MaxDamage)
            {
                return;
            }
            else
            {
                Explode();
            }
        }
        void Explode()
        {
            for (int i = 0; i < 25; i++)
            {
                Resources.SpawnResources(transform.position, "Radioactive Explosion Debris", false, null);
            }
            Resources.SpawnResources(transform.position, "Radioactive Explosion Cloud", false, null);
            ExplosionCreator.CreateFragmentationExplosion(new ExplosionCreator.ExplosionParameters(32, transform.position, 50f, 15f, true, ExplosionCreator.EffectSize.Large, 0.7f, 20));
            Destroy(gameObject);
        }
        void Update()
        {
        }
    }
}
public class FirstSpawn : MonoBehaviour
{

}