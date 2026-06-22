using System.Collections;

namespace WebApi.Tests.InlineData;

public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["es"];
        yield return ["pt"];
        yield return ["en"];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}