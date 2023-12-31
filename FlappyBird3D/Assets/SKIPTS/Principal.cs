using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Principal : MonoBehaviour
{
    public GameObject cerca;
    public GameObject nuvem;
    public GameObject pedra;
    public GameObject arbusto;
    public GameObject canos;

    public GameObject jogadorFelpudo;

    public GameObject particulas;

    bool comecou;
    bool acabou;
    public Text textoScore;
    int pontuacao;

    void Start(){
        Physics.gravity = new Vector3(0.0f, -20.0f, 0.0f);
        textoScore.text = "Toque para iniciar";
        //textoScore.fontSize = 30;
    }
    
    void Update()
    {
        if(Input.anyKeyDown){
            if(!acabou){
                if(!comecou){
                    comecou = true;
                    InvokeRepeating("CriaCerca", 1, 0.4f);
                    InvokeRepeating("CriaObstaculo", 1, 0.75f);

                    jogadorFelpudo.GetComponent<Rigidbody>().useGravity = true;
                    jogadorFelpudo.GetComponent<Rigidbody>().isKinematic = false;

                    textoScore.text = pontuacao.ToString();
                    textoScore.fontSize = 70;
                }
            VoaFelpudo();
            }
        }
        jogadorFelpudo.transform.rotation = Quaternion.Euler(jogadorFelpudo.GetComponent<Rigidbody>().velocity.y* -3,0,0); 
    }

    void CriaCerca()
    {
        Instantiate(cerca);
    }

    void CriaObstaculo()
    {
        int sorteiaObjeto = Random.Range(1,5);

        float posicaoXrandom = Random.Range(-2.8f, 2.53f);
        float posicaoYrandom = Random.Range(0.9f, 3.25f);
        float rotacaoYrandom = Random.Range(0.0f, 360.0f);

        GameObject novoObjeto = new GameObject();

        switch(sorteiaObjeto){
        case 1: novoObjeto = Instantiate(arbusto); posicaoYrandom = novoObjeto.transform.position.y; break;
        case 2: novoObjeto = Instantiate(pedra); posicaoYrandom = novoObjeto.transform.position.y;break;
        case 3: novoObjeto = Instantiate(nuvem); break;
        case 4: novoObjeto = Instantiate(canos); posicaoYrandom = Random.Range(-2.16f, 0.01f); posicaoXrandom = novoObjeto.transform.position.x; break;
        default: break;
        }

        novoObjeto.transform.position = new Vector3(posicaoXrandom, posicaoYrandom, novoObjeto.transform.position.z);
        novoObjeto.transform.rotation = Quaternion.Euler(novoObjeto.transform.rotation.x, rotacaoYrandom, novoObjeto.transform.rotation.y);
    }

    void VoaFelpudo(){
        GameObject novaParticula = Instantiate(particulas);
        novaParticula.transform.position = jogadorFelpudo.transform.position;

        jogadorFelpudo.GetComponent<Rigidbody>().velocity = Vector3.zero;
        jogadorFelpudo.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 10.0f,0.0f);
    }

    void MarcaPonto(){
        pontuacao++;
        textoScore.text = pontuacao.ToString();
    }
    void FimDeJogo(){
        acabou = true;
        CancelInvoke("CriaCerca");
        CancelInvoke("CriaObstaculo");
        Invoke("RecageraCena", 2);
    }
    void RecageraCena(){
        //Application.LoadLevel("SampleScene");
        SceneManager.LoadScene("SampleScene");
    } 

    
}
