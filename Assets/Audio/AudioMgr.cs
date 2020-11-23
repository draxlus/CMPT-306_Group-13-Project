using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 音频模块
/// </summary>
public class AudioMgr : BaseMgr<AudioMgr>
{
    //背景音乐
    private AudioSource bgM = null;
    //背景音乐音量
    private float BgmVolume = 1;
    //音效载体
    private GameObject soundObj =null;
    //音效列表
    private List<AudioSource> soundList = new List<AudioSource>();
    //音效音量
    private float soundVolume = 1;
    /// <summary>
    /// 构造函数
    /// </summary>
    public AudioMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(CheckSoundEnd);
    }
    /// <summary>
    /// 每帧检查音效是否结束
    /// </summary>
    public void CheckSoundEnd()
    {
        for(int i =soundList.Count-1;i > 0; --i)
        {
            //这个地方你写错了 应该是没有播放了 才销毁
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
                
            }
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBgm(string name)
    {
        if(bgM == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BGMobject";
            bgM = obj.AddComponent<AudioSource>();
        }
        ResMgr.GetInstance().LoadAsyn<AudioClip>("Music/Bgm/" + name, (audioClip) =>
        {
            bgM.clip = audioClip;
            bgM.loop = true;
            bgM.volume = BgmVolume;
            bgM.Play();

        });
    }
    /// <summary>
    /// 改变bgm音量
    /// </summary>
    /// <param name="value"></param>
    public void ChangeBgmVolumn(float value)
    {
        BgmVolume = value;
        if (bgM == null)
            return;
        bgM.volume = BgmVolume;
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBgm()
    {
        if (bgM == null)
            return;
        bgM.Pause();
    }
    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBgm()
    {
        if(bgM == null)
            return;
        bgM.Stop();
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(string name,bool isPlaying, UnityAction<AudioSource> callback =null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "SoundObject";
            
        }
        ResMgr.GetInstance().LoadAsyn<AudioClip>("Music/Sound/" + name, (audioClip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.loop = isPlaying;
            source.volume = soundVolume;
            source.Play();
            soundList.Add(source);
            if(callback!=null)
                callback(source);
        });
    }
    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// 改变所有音效音量
    /// </summary>
    /// <param name="v"></param>
    public void ChangeSoundVolume(float value)
    {
        soundVolume = value;
        for(int i =0; i < soundList.Count; ++i)
        {
            soundList[i].volume = soundVolume;
        }
    }
    
}

