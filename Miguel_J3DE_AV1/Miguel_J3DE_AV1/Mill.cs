using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Miguel_J3DE_AV1
{
    public class Mill
    {
        GraphicsDevice device;
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        Vector3 position;
        Texture2D texture;
        float angle, scale;
        BasicEffect effect;

        public Mill(GraphicsDevice device, Vector3 position,Texture2D texture, float angle = 0, float scale = 1) {
            this.device = device;
            this.position = position;
            this.texture = texture;
            this.angle = angle;
            this.scale = scale;

            this.effect = new BasicEffect(this.device);

            this.SetMatrix();
            this.CreateVertex();
            this.CreateVertexBuffer();
        }

        protected Matrix GetMatrix() 
        {
            return this.world;
        }

        private void SetMatrix() 
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angle));
            this.world *= Matrix.CreateTranslation(this.position);
        }

        public void SetMatrix(Matrix parentWorld) 
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angle));
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= parentWorld;
        }

        private void CreateVertex() 
        {
            this.verts = new VertexPositionTexture[] 
            {
                //front
                new VertexPositionTexture(new Vector3(-1,0,0),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,0),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,0,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,0,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,2,0),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,2,0),new Vector2(1,0)),
                //right         
                new VertexPositionTexture(new Vector3(1,0,0),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(1,2,0),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,0,-2),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,0,-2),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,2,0),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,2,-1),new Vector2(0.5f,0)),
                //left         
                new VertexPositionTexture(new Vector3(-1,0,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,0),new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0.5f,0)),
                new VertexPositionTexture(new Vector3(-1,2,0),new Vector2(1,0)),
                //back            
                new VertexPositionTexture(new Vector3(1,2,-1),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,0,-2),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(1,2,-1),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(1,1)),
                //top           
                new VertexPositionTexture(new Vector3(1,2,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,2,0),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0,0.5f)),
                new VertexPositionTexture(new Vector3(1,2,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0,0.5f)),
                new VertexPositionTexture(new Vector3(1,2,-1),new Vector2(1,0.5f)),
                //bottom          
                new VertexPositionTexture(new Vector3(1,0,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,0),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,0,0),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,-2),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,0,-2),new Vector2(1,0)),
                                  
            };                    
        }

        private void CreateVertexBuffer()
        {
            this.vBuffer = new VertexBuffer(this.device,
                                    typeof(VertexPositionTexture),
                                    this.verts.Length,
                                    BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(this.verts);
        }

        public virtual void Draw(Camera camera)
        {

            this.device.SetVertexBuffer(this.vBuffer);

            this.effect.World = this.world;
            this.effect.View = camera.GetView();
            this.effect.Projection = camera.GetProjection();
            this.effect.TextureEnabled = true;
            this.effect.Texture = this.texture;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                    this.verts,
                                                                    0, this.verts.Length / 3);
            }


        }
    }
}
