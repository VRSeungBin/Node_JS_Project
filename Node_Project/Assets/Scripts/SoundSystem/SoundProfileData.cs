using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioActionType
{
    Attack,
    Button,
    BGM
}

[CreateAssetMenu(fileName = "Sound Profile", menuName = "Containers/SoundProfile", order = 1)]
public class SoundProfileData : ScriptableObject            //스크립터블 오브젝트로 설정
{
    [SerializeField] private AudioActionType audioType;             //오디오 타입 설정
    [SerializeField] private List<AudioClip> randomClipList;        //리스트 보여줌

    public AudioActionType AudioType => audioType;
    public List<AudioClip> RandomClipList => randomClipList;
    public AudioClip GetRandomClip() => RandomClipList.Count > 0 ? RandomClipList.RandomItem() : null;
    public AudioClip GetClipIndex(int index) => RandomClipList.Count > index ? RandomClipList[index] : null;

}
