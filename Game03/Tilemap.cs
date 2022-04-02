using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game03
{
    public class Tilemap
    {
        /// <summary>
        /// dimensions of tiles and map
        /// </summary>
        int _tileWidth, _tileHeight, _mapWidth, _mapHeight;

        /// <summary>
        /// tileset texture
        /// </summary>
        Texture2D _tilesetTexture;

        /// <summary>
        /// tile infor for tileset
        /// </summary>
        Rectangle[] _tiles;

        /// <summary>
        /// tile map data
        /// </summary>
        int[] _map;

        /// <summary>
        /// filename of string
        /// </summary>
        string _filename;

        public Tilemap(string filename)
        {
            _filename = filename;
        }

        /// <summary>
        /// load up everything we need for tilemap
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var lines = data.Split('\n');

            //first line in tileset filename
            var tilesetFilename = lines[0].Trim();
            _tilesetTexture = content.Load<Texture2D>(tilesetFilename);

            //second line is tile size
            var secondline = lines[1].Split(',');
            _tileWidth = int.Parse(secondline[0]);
            _tileHeight = int.Parse(secondline[1]);

            // determine tile bounds
            int tilesetColumns = _tilesetTexture.Width / _tileWidth;
            int tilesetRows = _tilesetTexture.Height / _tileHeight;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];

            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {
                    int index = y * tilesetColumns + x;
                     _tiles[index] = new Rectangle(
                        x * _tileWidth,
                        y * _tileHeight,
                        _tileWidth,
                        _tileHeight
                        );
                }
            }

            //third line is map size
            var thirdline = lines[2].Split(',');
            _mapWidth = int.Parse(thirdline[0]);
            _mapHeight = int.Parse(thirdline[1]);

            //create map
            var fourthline = lines[3].Split(',');
            _map = new int[_mapWidth * _mapHeight];
            for (int i = 0; i < _mapWidth * _mapHeight; i++)
            {
                _map[i] = int.Parse(fourthline[i]);
            }
        }

        /// <summary>
        /// draw our tilemap 
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    int index = _map[y * _mapWidth + x] - 1;
                    if (index <= -1) continue;
                    spriteBatch.Draw(
                        _tilesetTexture,
                        new Vector2(
                            x * _tileWidth,
                            y * _tileHeight
                            ),
                        _tiles[index],
                        Color.White
                        );
                }
            }
        }
    }
}
