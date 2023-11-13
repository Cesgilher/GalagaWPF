using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalagaWPF.Models;

public class ProjectileManager
{
    private List<Projectile> projectiles;
    private DBContext dB = new DBContext();

    public ProjectileManager()
    {

    }
    //public List<Projectile> GetProjectiles()
    //{
    //    projectiles = dB.Projectiles.ToList();
    //    return projectiles;
    //}
}
