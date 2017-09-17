using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utils {
    public static class Utils {

        public static void DrawText(SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Vector2 size, Color color, float rotation) {
            Vector2 measure = font.MeasureString(text);
            float scale = MathHelper.Min(size.X / measure.X, size.Y / measure.Y);
            spriteBatch.DrawString(font, text, position, color, rotation, measure / 2f, scale, SpriteEffects.None, 0);
        }

    }
}
