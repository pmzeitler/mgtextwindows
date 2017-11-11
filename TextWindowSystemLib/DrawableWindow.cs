
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace net.PhoebeZeitler.TextWindowSystem
{
    interface DrawableWindow
    {
        bool Draw(SpriteBatch sb);
        bool Draw(SpriteBatch sb, Color tintValue);
        bool Draw(SpriteBatch sb, Color tintValue, int transparency);

    }
}
