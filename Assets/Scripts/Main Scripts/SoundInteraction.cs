using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInteraction : MonoBehaviour {

    //nova tag & layer

    [SerializeField] private float maxVolume;
    [SerializeField] private float minVolume = 0f;
    [SerializeField] private float stepVolume = 0.02f; //how smooth is the transition per frame
    [SerializeField] private AudioSource jungleSound;

    private float currentVolume = 0f;
    private float targetVolume = 0f;
    private float initialVolume;

    private Collider zoneCollider;
    private Vector3 currentZonePosition;

    private bool inZone = false;
    private float distanceToCenter;

	void Start ()
    {
        initialVolume = jungleSound.volume;

        maxVolume = initialVolume;
	}
	
	void Update ()
    {
        SoundChange();
	}

    #region Functions

    private void SoundChange()
    {
        if (inZone)
        {
            currentZonePosition = zoneCollider.bounds.center;
            Vector3 posRelativeToCenter = this.transform.position - currentZonePosition;

            //qual coordenada esta mais proxima de alguma parede?
            //Retornando a posição do jogador, n muda muito
            //Vector3 closestWall = zoneCollider.bounds.ClosestPoint(this.transform.position);

            Vector2 playerDistToWalls = new Vector2(Mathf.Abs(Mathf.Abs(posRelativeToCenter.x) - zoneCollider.bounds.extents.x),
            Mathf.Abs(Mathf.Abs(posRelativeToCenter.z) - zoneCollider.bounds.extents.z));

            //deixar tudo relativo ao centro do collider
            //Vector3 closestWallRelativeToZoneCenter = closestWall - zoneCollider.bounds.center;

            //o player ta mais perto da parede de X ou Y?
            //Vector3 distanceToTheWall = closestWallRelativeToZoneCenter - posRelativeToCenter;

            float shortestDistance = float.MaxValue;
            float percentDistance = 0f;
            if (playerDistToWalls.x < playerDistToWalls.y)
            {
                shortestDistance = playerDistToWalls.x;
                percentDistance = Mathf.Clamp01(playerDistToWalls.x / zoneCollider.bounds.extents.x); //se a escala ficar estranha, talvez seja bounds.extents.x aqui
            }
            else
            {
                shortestDistance = playerDistToWalls.y;
                percentDistance = Mathf.Clamp01(playerDistToWalls.y / zoneCollider.bounds.extents.z); //se a escala ficar estranha, talvez seja bounds.extents.y aqui
            }

            //teoricamente ja tenho a menor distancia ateh a parede mais proxima
            //escalar isso com o som
            //quanto mais perto da parede, menor o volume do som, numa escala entre min e max.
            targetVolume = Mathf.Clamp(percentDistance, maxVolume, minVolume);

            //testar até aqui pra ver se ele muda o volume certinho. pra baixo vou fazer ele mudar gradativamente. ou tentar
            currentVolume = (targetVolume > currentVolume) ? (currentVolume + stepVolume) : (currentVolume - stepVolume);

            //assinalar o current volume ao audio source. o ideal eh colocar no audio mixer e pegar a referencia pelo mixer. nao eh dificil, mas n lembro como faz e to com preguiça.
            jungleSound.volume = currentVolume;
        }
        else
        {
            jungleSound.volume = initialVolume;
        }
    }

    #endregion

    #region Triggers
    private void OnTriggerEnter(Collider _col)
    {
        if(_col.CompareTag(Globals.SOUND_ZONE_TAG))
        {
            inZone = true;
            zoneCollider = _col;
        }
    }

    private void OnTriggerExit(Collider _col)
    {
        if (_col.CompareTag(Globals.SOUND_ZONE_TAG))
        {
            inZone = false;
            zoneCollider = null;
        }
    }
    #endregion
}
