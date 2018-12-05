using NSubstitute;
using NUnit.Framework;
using UnityEngine.UI;

/*  ================= Unit test for Music =================
 *  -Uses NSubstitute library: https://github.com/nsubstitute/nsubstitute
 *  -References:
 *      -https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/
 *      -How to use NSubstitute in Unity: https://www.youtube.com/watch?v=xSa2S-W7x48
 */

public class MusicUnitTest
{
    [Test]
    public void test_a_sound_created_and_not_null()
    {
        var soundMock = GetSoundMock();
        Assert.That(soundMock, Is.InstanceOf(typeof(Sound)));
    }

    [Test]
    public void test_b_audio_manager_created_and_not_null()
    {
        var audioManagerMock = GetAudioManagerMock();
        Assert.That(audioManagerMock, Is.InstanceOf(typeof(AudioManager)));
    }

    [Test]
    public void test_c_add_song()
    {
        var audioManagerMock = GetAudioManagerMock();
        audioManagerMock.sounds[0] = GetSoundMock();
        Assert.That(audioManagerMock.sounds.Length, Is.EqualTo(1));
    }

    private static Sound GetSoundMock()
    {
        var soundMock = Substitute.For<Sound>();
        return soundMock;
    }

    private static AudioManager GetAudioManagerMock()
    {
        var audioManagerMock = Substitute.For<AudioManager>();
        audioManagerMock.sounds = new Sound[1];
        audioManagerMock.sounds[0] = GetSoundMock();
        return audioManagerMock;
    }
}
