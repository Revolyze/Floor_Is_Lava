using Floor_Is_Lava.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Floor_Is_Lava.Items
{
    public class MyPlayer : ModPlayer
    {
        public bool isTouchingLava;
        private DateTime? dateTime;
        private DateTime timeSinceHit;
        private bool beginFloorIsLava;

        public override void UpdateDead()
        {
            player.ClearBuff(ModContent.BuffType<FloorIsLava>());
            base.UpdateDead();
        }

        public override void PreUpdate()
        {
            if (dateTime == null)
            {
                Main.NewText("Floor Is Lava! Stay away from blocks that are not man-made!");
                Main.NewText("You have -20- seconds until the floor begins to hurt. Collect some wood to stand on!");
                dateTime = DateTime.Now;
            }

            TimeSpan timeSpan = DateTime.Now - (DateTime)dateTime;
            if (timeSpan.TotalSeconds < 20)    
                return;
            

            if (!beginFloorIsLava)
            {
                Main.NewText("The floor is lava!");
                beginFloorIsLava = true;
            }

            var entityEdgeTiles = Collision.GetEntityEdgeTiles(player);
            foreach (var tile in entityEdgeTiles)
            {
                if (!player.wet && !player.dead)
                {
                    if (Main.tile[tile.X, tile.Y].active() &&
                       (Main.tile[tile.X, tile.Y].type == TileID.Dirt || Main.tile[tile.X, tile.Y].type == TileID.Grass ||
                        Main.tile[tile.X, tile.Y].type == TileID.Sand || Main.tile[tile.X, tile.Y].type == TileID.Sandstone ||
                        Main.tile[tile.X, tile.Y].type == TileID.SnowBlock || /*Main.tile[tile.X, tile.Y].type == TileID.IceBlock ||*/
                        Main.tile[tile.X, tile.Y].type == TileID.Mud || Main.tile[tile.X, tile.Y].type == TileID.CorruptGrass ||
                        Main.tile[tile.X, tile.Y].type == TileID.Ash || Main.tile[tile.X, tile.Y].type == TileID.ClayBlock ||
                        Main.tile[tile.X, tile.Y].type == TileID.Stone || Main.tile[tile.X, tile.Y].type == TileID.Ebonsand ||
                        Main.tile[tile.X, tile.Y].type == TileID.Ebonstone || Main.tile[tile.X, tile.Y].type == TileID.Silt ||
                        Main.tile[tile.X, tile.Y].type == TileID.Pearlsand || Main.tile[tile.X, tile.Y].type == TileID.Pearlstone ||
                        Main.tile[tile.X, tile.Y].type == TileID.Slush || Main.tile[tile.X, tile.Y].type == TileID.GraniteBlock ||
                        Main.tile[tile.X, tile.Y].type == TileID.MarbleBlock || Main.tile[tile.X, tile.Y].type == TileID.GraniteBlock ||
                        Main.tile[tile.X, tile.Y].type == TileID.HardenedSand || Main.tile[tile.X, tile.Y].type == TileID.CorruptHardenedSand ||
                        Main.tile[tile.X, tile.Y].type == TileID.CrimsonHardenedSand || Main.tile[tile.X, tile.Y].type == TileID.HallowHardenedSand ||
                        Main.tile[tile.X, tile.Y].type == TileID.CrimsonSandstone || Main.tile[tile.X, tile.Y].type == TileID.CorruptSandstone ||
                        Main.tile[tile.X, tile.Y].type == TileID.Crimstone))
                    {
                        TimeSpan ts = DateTime.Now - timeSinceHit;
                        if (ts.TotalSeconds > 1) {
                            Main.PlaySound(SoundID.PlayerHit, player.position);
                            timeSinceHit = DateTime.Now;
                            player.AddBuff(ModContent.BuffType<FloorIsLava>(), 100, quiet: false);
                        }
                        break;
                    }
                }
            }
            base.PreUpdate();
        }
    }
}