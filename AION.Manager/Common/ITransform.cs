namespace AION.BL.Common
{
    internal interface ITransform<Source,Destination>
    {
        Destination Transform(Source source);
    }
}