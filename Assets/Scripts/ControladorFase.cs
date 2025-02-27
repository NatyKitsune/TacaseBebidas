using UnityEngine;
using UnityEngine.UI;

public class ControladorFase : MonoBehaviour
{
    internal float TempoRestante;

    public GameObject telaGanhou, telaPerdeuErrou, telaPerdeuTempo, TelaPause;


    public Image imagemTacaSelecionada;
    public Text textoTempoRestante, textoFaseAtual;

    //Vetores das imagens das garrafas e dos tipos das bebidas
    public Sprite[] bebidas;
    public string[] tipos;

    public GameObject personagem;
    public SpriteRenderer bebidaNaTela;

    //Variaveis Controle
    internal int faseAtual, numBebidaAtual;
    internal string nomeBebidaAtual, nomeTacaAtual;
    internal Vector3 posInicialPersonagem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posInicialPersonagem = personagem.transform.localPosition;
        TempoRestante = 60;
        faseAtual = 1;
        nomeTacaAtual = "";
        EscolherUmaBebida();
        
        imagemTacaSelecionada.sprite = null;

    }

    // Update is called once per frame
    void Update()
    {
        //Código para diminuir tempo
        TempoRestante -= Time.deltaTime;
        //Código para atualizar os textos na tela 

        textoTempoRestante.text = "Tempo Restante:" + TempoRestante.ToString("00");
        textoFaseAtual.text = "Fase: " + faseAtual;

        // Verificar se o tempo acabou

        if (TempoRestante <= 0)
        {
            telaPerdeuTempo.SetActive(true);
            Time.timeScale = 0;
            TempoRestante = 0;
        }
    }
    public void PegarTaca(GameObject taca)
    {
        imagemTacaSelecionada.sprite = taca.GetComponent<SpriteRenderer>().sprite;
        imagemTacaSelecionada.preserveAspect = true;
        nomeTacaAtual = taca.GetComponent<ControladorTacas>().tipo;
    }
    public void Comparar()
    {
        if (nomeTacaAtual == nomeBebidaAtual)
        {
            telaGanhou.SetActive(true);
            Time.timeScale = 0;
        }
        else if (nomeTacaAtual != "")
        {
            telaPerdeuErrou.SetActive(true);
            Time.timeScale = 0;
        }

    }
    public void EscolherUmaBebida()
    {
        int valorAleatorio = (int)(Random.value * bebidas.Length);

        if (numBebidaAtual == valorAleatorio)
            valorAleatorio++;

        bebidaNaTela.sprite = bebidas[valorAleatorio];
        nomeBebidaAtual = tipos[valorAleatorio];
    }
    public void Pausar()
    {
        TelaPause.SetActive(true );
        Time.timeScale = 0;
    }
    public void Despausar()
    {
        TelaPause.SetActive(false );
        Time.timeScale = 1;
    }    

    public void AvancarFase()
    {
        faseAtual += 1;

        personagem.transform.localPosition = posInicialPersonagem;
        TempoRestante += 10;

        nomeTacaAtual = "";
        EscolherUmaBebida();

        imagemTacaSelecionada.sprite = null;

        telaGanhou.SetActive(false );
        Time.timeScale = 1;
    }
    public void RecomecarFase()
    {
        faseAtual = 1;

        personagem.transform.localPosition = posInicialPersonagem;
        TempoRestante = 60;

        nomeTacaAtual = "";
        EscolherUmaBebida();

        imagemTacaSelecionada.sprite = null;

        telaPerdeuErrou.SetActive(false);
        telaPerdeuTempo.SetActive(false);
        Time.timeScale = 1;
    }
}
