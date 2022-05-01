using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





public static class DamageCalculation
{
    public static int damageEquation(float attackerAttack, float defenderDefense)
    {
        return (int)(attackerAttack * (100 / (100 + defenderDefense)));
    }
}

