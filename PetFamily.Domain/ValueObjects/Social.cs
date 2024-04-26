using CSharpFunctionalExtensions;
using PetFamily.Domain.Common;

namespace PetFamily.Domain.ValueObjects;

public record Social
{
    public static readonly Social Telegram = new(nameof(Telegram).ToUpper());
    public static readonly Social Whatsapp = new(nameof(Whatsapp).ToUpper());
    public static readonly Social Viber = new(nameof(Viber).ToUpper());
    public static readonly Social Vkontakte = new(nameof(Vkontakte).ToUpper());
    public static readonly Social Odnoklassniki = new(nameof(Odnoklassniki).ToUpper());
    public static readonly Social Youtube = new(nameof(Youtube).ToUpper());
    public static readonly Social Tiktok = new(nameof(Tiktok).ToUpper());
    public static readonly Social Instagram = new(nameof(Instagram).ToUpper());

    private static readonly Social[] _all =
        [Telegram, Whatsapp, Viber, Vkontakte, Odnoklassniki, Youtube, Tiktok, Instagram];

    public string Value { get; }

    private Social(string value)
    {
        Value = value;
    }

    public static Result<Social, Error> Create(string input)
    {
        if (input.IsEmpty())
            return Errors.General.ValueIsRequried("input");

        var social = input.Trim().ToUpper();

        if (_all.Any(s => s.Value == social) == false)
        {
            return Errors.General.ValueIsInvalid("social");
        }

        return new Social(social);
    }
}