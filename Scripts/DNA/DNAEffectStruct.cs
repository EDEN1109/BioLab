public struct DNAEffectStruct
{
    public Effect effect;
    public bool isHide;
    public int power;

    public DNAEffectStruct(Effect effect, bool isHide, int power)
    {
        this.effect = effect;
        this.isHide = isHide;
        this.power = power;
    }
}
