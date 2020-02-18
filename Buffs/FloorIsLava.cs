using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Floor_Is_Lava.Items;

namespace Floor_Is_Lava.Buffs
{
    public class FloorIsLava : ModBuff
    {
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Floor is lava!");
			Description.SetDefault("Get off the ground, it burns! Anything not man-made will likely kill you.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen -= 60;
		}
	}
}