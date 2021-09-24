using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Miguel_J3DE_AV1
{
    public class Camera
    {
        Matrix view, projection;
        Vector3 position, target, up;
        Point sizeScreen;
        float speedT, speedR, angle;

        public Camera(Vector3 position, Vector3 target, Point sizeScreen)
        {
            this.position = position;
            this.target = target;
            this.up = Vector3.Up;
            this.sizeScreen = sizeScreen;

            this.SetView();
            this.SetProjection();

            this.speedT = 10;
            this.speedR = 100;
            this.angle = 0;
        }

        private void SetView()
        {
            this.view = Matrix.CreateLookAt(this.position, this.target, this.up);
        }

        public Matrix GetView()
        {
            return this.view;
        }

        private void SetProjection()
        {
            this.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                  this.sizeScreen.X / (float)this.sizeScreen.Y,
                                                                  0.1f, 1000);
  
        }

        public Matrix GetProjection()
        {
            return this.projection;
        }

        public void Update(GameTime gameTime) 
        { 
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if (Keyboard.GetState().IsKeyDown(Keys.Q)) angle += speedR * deltaTime;
            if (Keyboard.GetState().IsKeyDown(Keys.E)) angle -= speedR * deltaTime;
            if (Keyboard.GetState().IsKeyDown(Keys.W)) 
            { 
                this.position.X -=(float)Math.Sin(MathHelper.ToRadians(angle)) * deltaTime * speedT;
                this.position.Z -= (float)Math.Cos(MathHelper.ToRadians(angle)) * deltaTime * speedT;
            }  
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {   
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(angle)) * deltaTime * speedT;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(angle)) * deltaTime * speedT;
            }   
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {   
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(angle-90)) * deltaTime * speedT;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(angle-90)) * deltaTime * speedT;
            }   
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {   
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(angle+90)) * deltaTime * speedT;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(angle+90)) * deltaTime * speedT;
            }

            this.view = Matrix.Identity;
            this.view *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angle));
            this.view *= Matrix.CreateTranslation(this.position);
            this.view = Matrix.Invert(this.view);
        } 
    }
}
