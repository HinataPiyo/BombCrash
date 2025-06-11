using UnityEngine;
using UnityEngine.Playables;

public class StoryController : MonoBehaviour
{
    public PlayableDirector timeline;
    [SerializeField] Animator stageBackgroundAnim;

    void Start()
    {
        stageBackgroundAnim.SetBool("RedBlink", true);
    }

    public void StartStory()
    {
        timeline.Play();
    }

    public void PauseStory()
    {
        timeline.Pause();
    }

    public void ResumeStory()
    {
        timeline.Resume();
    }
}
