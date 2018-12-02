using NSubstitute;
using NUnit.Framework;
using UnityEngine.UI;

/*  ================= Unit test for UI and buttons =================
 *  -Uses NSubstitute library: https://github.com/nsubstitute/nsubstitute
 *  -References:
 *      -https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/
 *      -How to use NSubstitute in Unity: https://www.youtube.com/watch?v=xSa2S-W7x48
 */

public class UIUnitTest
{
    [Test]
    public void test_a_button_created_and_not_null() // Checks to see if buttons can be created
    {
        var buttonMock = GetButtonMock();
        Assert.That(buttonMock, Is.InstanceOf(typeof(Button)));
    }

    [Test]
    public void test_b_text_created_and_not_null() // Checks to see if text can be created
    {
        var textMock = GetTextMock();
        Assert.That(textMock, Is.InstanceOf(typeof(Text)));
    }

    private static Text GetTextMock()
    {
        var textMock = Substitute.For<Text>();
        return textMock;
    }

    private static Button GetButtonMock()
    {
        var buttonMock = Substitute.For<Button>();
        return buttonMock;
    }
}
