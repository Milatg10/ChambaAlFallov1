using System.Collections.Generic;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A system for batch animation of fading sprites.
    /// </summary>
    public class FadingSpriteSystem : MonoBehaviour
    {
        void Update()
        {
            foreach (var c in FadingSprite.Instances)
            {
                // --- ARREGLO ---
                // Si el objeto 'c' es nulo (se destruyó al cambiar de escena),
                // pasamos al siguiente ('continue') para no provocar el error.
                if (c == null) continue;
                // ----------------

                if (c.gameObject.activeSelf)
                {
                    c.alpha = Mathf.SmoothDamp(c.alpha, c.targetAlpha, ref c.velocity, 0.1f, 1f);
                    c.spriteRenderer.color = new Color(1, 1, 1, c.alpha);
                }
            }
        }
    }
}