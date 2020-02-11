using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class Attack
{
    public enum DamageType { Slicing, Blunt }
    public int Damage { get; set; }
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public DamageType TypeOfDamage { get; set; }


}
