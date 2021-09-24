using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Miguel_J3DE_AV1
{
    public class Mundo 
    {
        GraphicsDevice device;
        Matrix world;
        Vector3 position , displacementPos;

        float scale, angle, speed, angleY;
        Ground ground;
        Sail sailR,sailL;
        House house;

        public Mundo(GraphicsDevice device, Vector3 position, Texture2D millText,
                     Texture2D sailText, Texture2D houseText, Texture2D groundText, Vector3 displacementPos,
                     float angle = 0, float scale = 1, float speed = 100f) 
        {
            this.speed = speed;
            this.device = device;
            this.position = position;
            this.angleY = angle;
            this.scale = scale;
            this.displacementPos = displacementPos;
            this.ground = new Ground(this.device, Vector3.Zero * scale, groundText, 0 + this.angle, 10 * scale);
            this.house = new House(this.device, Vector3.UnitZ * 2 * scale, houseText, 0.75f * scale);
            this.sailR = new Sail(this.device, Vector3.UnitX * 5 * scale , sailText, millText, -45, 1.5f * scale, 100);
            this.sailL = new Sail(this.device, Vector3.UnitX * -5 * scale, sailText, millText, 45, 1.5f * scale, 100);

            SetMatrix();
        }

        public Matrix GetMatrix() 
        {
            return this.world;
        }

        public virtual void SetMatrix() 
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angle));
        }



        public virtual void Update(GameTime gameTime) 
        {
            

            this.sailR.Update(gameTime);
            this.sailL.Update(gameTime);

            this.angle += this.speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            this.world = Matrix.Identity;


            

            this.world *= Matrix.CreateRotationZ(MathHelper.ToRadians(-this.angle));

            this.world *= Matrix.CreateTranslation(this.displacementPos);

            this.world *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.angle));

            this.world *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angleY));
            this.world *= Matrix.CreateTranslation(this.position);
            

            this.ground.SetMatrix(GetMatrix());
            this.sailR.SetMatrix(GetMatrix());
            this.sailL.SetMatrix(GetMatrix());
            this.house.SetMatrix(GetMatrix());
        }          

        public virtual void Draw(Camera camera) 
        {
            this.ground.Draw(camera);
            this.sailR.Draw(camera);
            this.sailL.Draw(camera);
            this.house.Draw(camera);
        }
    }
}
