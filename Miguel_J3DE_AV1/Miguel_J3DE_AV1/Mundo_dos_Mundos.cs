using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Miguel_J3DE_AV1
{
    public class Mundo_dos_Mundos : Mundo

    {
        GraphicsDevice device;
        Matrix world;
        Vector3 position;
        float speed, scale, angle;

        Mundo[] mundos; 

        public Mundo_dos_Mundos(GraphicsDevice device, Vector3 position, Texture2D millText, Texture2D sailText, 
                                Texture2D houseText, Texture2D groundText, Vector3 displacementPos,float angle = 0, float scale = 1f, 
                                float speed = 20f) : base(device, position, millText, sailText, houseText, groundText, 
                                displacementPos, angle, scale, speed) 
        {
            this.device = device;
            this.speed = speed;
            this.scale = scale;
            this.angle = 0;

            CreateMundos(millText, sailText, houseText, groundText);
            foreach (Mundo m in mundos) m.SetMatrix();
            this.SetMatrix();
        }

        private void CreateMundos(Texture2D millText, Texture2D sailText, 
                                Texture2D houseText, Texture2D groundText) 
        {
            float s = 0.05f;
            mundos = new Mundo[] 
            {
                //left
                new Mundo(this.device, (Vector3.UnitX * -5) + (Vector3.UnitY * 2.25f)   * this.scale, millText, sailText,houseText,groundText,new Vector3(0,-1.2f,0.6f )*1.2f,45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * -5) + (Vector3.UnitY * 2.25f)   * this.scale, millText, sailText,houseText,groundText,new Vector3(-1.2f,0,0.6f )*1.2f,45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * -5) + (Vector3.UnitY * 2.25f)   * this.scale, millText, sailText,houseText,groundText,new Vector3(0, 1.2f,0.6f )*1.2f,45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * -5) + (Vector3.UnitY * 2.25f)   * this.scale, millText, sailText,houseText,groundText,new Vector3(1.2f, 0,0.6f )*1.2f,45f,s,this.speed * 5),
                                                                                                                                                                   
                //right                                                                                                                                             
                new Mundo(this.device, (Vector3.UnitX * 5)  + (Vector3.UnitY * 2.25f)  * this.scale, millText, sailText,houseText,groundText,new Vector3(0,-1.2f, 0.6f )*1.2f,-45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * 5)  + (Vector3.UnitY * 2.25f)  * this.scale, millText, sailText,houseText,groundText,new Vector3(-1.2f,0, 0.6f )*1.2f,-45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * 5)  + (Vector3.UnitY * 2.25f)  * this.scale, millText, sailText,houseText,groundText,new Vector3(0, 1.2f, 0.6f )*1.2f,-45f,s,this.speed * 5),
                new Mundo(this.device, (Vector3.UnitX * 5)  + (Vector3.UnitY * 2.25f)  * this.scale, millText, sailText,houseText,groundText,new Vector3(1.2f, 0, 0.6f )*1.2f,-45f,s,this.speed * 5),
            };
        }

        

        public override void SetMatrix()
        {
            base.SetMatrix();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Mundo m in mundos) m.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(Camera camera)
        {
            foreach (Mundo m in mundos) m.Draw(camera);
            base.Draw(camera);
        }
            
    }
}
