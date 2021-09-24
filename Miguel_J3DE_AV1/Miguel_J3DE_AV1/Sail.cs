using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Miguel_J3DE_AV1
{
    public class Sail : Mill
    {
        GraphicsDevice device;
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        Vector3 position;
        Texture2D texture;
        float angle, speed;
        BasicEffect effect;

        public Sail(GraphicsDevice device, Vector3 position, Texture2D sailText, Texture2D millText, float angle = 0, float scale = 1, float speed = 0)
            : base(device, position, millText, angle, scale) 
        {
            this.device = device;
            this.position = new Vector3(0,1.5f,0.15f);
            this.angle = 0;
            this.speed = speed;
            this.texture = sailText;

            this.effect = new BasicEffect(this.device);

            this.SetMatrix();
            this.CreateVertex();
            this.CreateVertexBuffer();
            
        }

        
        private void SetMatrix() 
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.angle));
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= base.GetMatrix(); 
        }

        public void SetMatrix(Matrix parentWorld) 
        {
            
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.angle));
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= base.GetMatrix();
            
            base.SetMatrix(parentWorld);
        }

        private void CreateVertex() 
        {
            this.verts = new VertexPositionTexture[] 
            {
                //down
                new VertexPositionTexture(new Vector3(0,0,0),new Vector2(0.5f,1)),
                new VertexPositionTexture(new Vector3(0.25f,-1,0),new Vector2(0.75f,0)),
                new VertexPositionTexture(new Vector3(-0.25f,-1,0),new Vector2(0.25f,0)),
                //up
                new VertexPositionTexture(new Vector3(0,0,0),new Vector2(0.5f,1)),
                new VertexPositionTexture(new Vector3(-0.25f,1,0),new Vector2(0.25f,0)),
                new VertexPositionTexture(new Vector3(0.25f,1,0),new Vector2(0.75f,0)),
                //left
                new VertexPositionTexture(new Vector3(0,0,0),new Vector2(0.5f,1)),
                new VertexPositionTexture(new Vector3(-1,-0.25f,0),new Vector2(0.75f,0)),
                new VertexPositionTexture(new Vector3(-1,0.25f,0),new Vector2(0.25f,0)),
                //right
                new VertexPositionTexture(new Vector3(0,0,0),new Vector2(0.5f,1)),
                new VertexPositionTexture(new Vector3(1,0.25f,0),new Vector2(0.25f,0)),
                new VertexPositionTexture(new Vector3(1,-0.25f,0),new Vector2(0.75f,0)),
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

        public void Update(GameTime gameTime) {
            angle += this.speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            this.SetMatrix();
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);

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
