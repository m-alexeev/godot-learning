using System;

namespace spacewar.scripts.player.enums;

public enum Weapon {
   LASER,
   CANNON,
   ROCKET,
}

public static class WeaponUtils {
   public static int ToVariant(Weapon weapon) {
      return (int)weapon;
   }

   public static Weapon FromVariant(int variant) {
      return (Weapon)variant;
   } 
}