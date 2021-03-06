using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Miguel_J3DE_AV1
{
    public class House
    {
        GraphicsDevice device;
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        Texture2D texture;
        Vector3 position;
        float scale, angleY;
        BasicEffect effect;

        public House(GraphicsDevice device, Vector3 position, Texture2D texture, float scale = 1) 
        {
            this.device = device;
            this.position = position;
            this.texture = texture;
            this.scale = scale;

            this.effect = new BasicEffect(this.device);

            this.SetMatrix();
            this.CreateVertex();
            this.CreateVertexBuffer();
     
        }

        //public void SetAngleY(float angle)
        //{
        //    this.angleY = angle;
        //}

        //public void SetPosition(Vector3 position)
        //{
        //    this.position = position;
        //}

        public Matrix GetMatrix() 
        {
            return this.world;
        }

        public void SetMatrix() 
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateTranslation(this.position);
        }

        public void SetMatrix(Matrix parentWorld)
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= parentWorld;
        }

        private void CreateVertex() 
        {
            this.verts = new VertexPositionTexture[] 
            { 
                //front
                new VertexPositionTexture(new Vector3(1,0,1),  new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,1), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,1), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,2,1), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,2,1),  new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(1,0,1),  new Vector2(1,1)),
                //right                                                        
                new VertexPositionTexture(new Vector3(1,0,1),  new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(1,2,1),  new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,0,-1), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,2,1),  new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,2,-1), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(1,0,-1), new Vector2(1,1)),
                //left                                                         
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,2,1), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-1,0,-1),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,0,-1),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,1), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,0,1), new Vector2(1,1)),
                //back                                                         
                new VertexPositionTexture(new Vector3(1,0,-1), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(1,2,-1), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,0,-1),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,-1),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(1,2,-1), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(1,0)),
                //top                                                          
                new VertexPositionTexture(new Vector3(1,2,1),  new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,2,1), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-1,2,-1),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(1,2,-1), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(1,2,1),  new Vector2(1,1)),
                //down                                                         
                new VertexPositionTexture(new Vector3(1,0,1),  new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-1,0,1), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(1,0,-1), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(1,0,-1), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-1,0,1), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-1,0,-1),new Vector2(0,0)),
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

        public void Update(GameTime gameTime) {}

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
