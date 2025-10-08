using UnityEngine;
using System.IO;
using SQLite;
using System.Collections;
using System.Collections.Generic;


public class GameDatabase : MonoBehaviour
{
    public List<string> powerUpsList = new List<string> { "Gunner", "Destroyer", "CoinMagnet" };

    private SQLiteConnection db;

    void Awake()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "savegame.db");
        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Banco criado/carregado em: " + dbPath);

        db.CreateTable<Configuracoes>();
        db.CreateTable<Progresso>();
        db.CreateTable<PowerUpsTable>();

        // Se ainda não existir dados, cria com valores padrão
        if (db.Table<Configuracoes>().Count() == 0)
        {
            CriarConfiguracoes(1f, 1f, "3120x1440", true, true);
        }

        if (db.Table<Progresso>().Count() == 0)
        {
            CriarProgresso(1, 0);
        }

        if (db.Table<PowerUpsTable>().Count() == 0)
        {
            foreach (string name in powerUpsList)
            {
                CriarPowerUps(1, name, 0, 5f);
            }
        }
        
        /* Exemplo de como chamar as tabelas
        Configuracoes config = CarregarConfiguracoes();

        Progresso prog = CarregarProgresso(1);

        PowerUpsTable powerUp = CarregarPowerUps(1, "Gunner");

        float vol = config.VolumeMusica;
        int score = prog.ScoreRecord;
        string dur = powerUp.NamePower;

        Debug.Log(vol);
        Debug.Log(score);
        Debug.Log(dur);*/

    }

    // ---------------- CRIAR NOVA CONFIG -----------------------
    public void CriarConfiguracoes(float volumeMusica, float volumeEfeitos, string resolucao, bool telaCheia, bool defaultSet ) 
    {
        db.Insert(new Configuracoes
        {
            VolumeMusica = volumeMusica,
            VolumeEfeitos = volumeEfeitos,
            Resolucao = resolucao,
            TelaCheia = telaCheia ? 1: 0,
            DefaultSave = defaultSet ? 1 : 0
        });
    }

    // ---------------- ATUALIZAR CONFIGURAÇÕES ----------------
    public void AtualizarConfiguracoes(int id, float volumeMusica, float volumeEfeitos, string resolucao, bool telaCheia)
    {
        db.Execute("UPDATE Configuracoes SET VolumeMusica = ?, VolumeEfeitos = ?, Resolucao = ?, TelaCheia = ? WHERE Id = ?", volumeMusica, volumeEfeitos, resolucao, (telaCheia ? 1 : 0), id);
    }

    public Configuracoes CarregarConfiguracoes()
    {
        return db.Table<Configuracoes>().Where(c => c.DefaultSave == 1).FirstOrDefault();
    }

    // ------------- CRIAR PROGRESSO -------------
    public void CriarProgresso(int score, int coins)
    {
        db.Insert(new Progresso
        {
            ScoreRecord = score,
            Coins = coins
        });
    }

    // ---------------- PROGRESSO ----------------
    public void SalvarProgresso(int id, int scoreRecord, int coins)
    {
        db.Execute("UPDATE Progresso SET ScoreRecord = ?, Coins = ? WHERE IdSave = ?", scoreRecord, coins, id);
    }

    // ----------- CARREGAR PROGRESSO ------------
    public Progresso CarregarProgresso(int id)
    {
        return db.Find<Progresso>(id);
    }

    // ------------ CRIAR POWER UPS --------------
    public void CriarPowerUps(int idSave, string powerUpName, int nivel, float duracao)
    {
        db.Insert(new PowerUpsTable
        {
            IdSave = idSave,
            NamePower = powerUpName,
            Nivel = nivel,
            Duracao = duracao
        });
    }

    // ---------------- POWER UPS ----------------
    public void SalvarPowerUps(int idSave, string name, int nivel, float duracao)
    {
        db.Execute("UPDATE PowerUpsTable SET Nivel = ?, Duracao = ? WHERE IdSave = ? AND NamePower = ?", nivel, duracao, idSave, name);
    }

    public PowerUpsTable CarregarPowerUps(int idSave, string name)
    {
        return db.Table<PowerUpsTable>().Where(p => p.IdSave == idSave && p.NamePower == name).FirstOrDefault();
    }

    void OnDestroy()
    {
        db?.Close();
    }
}

// ---------------- MODELOS ----------------
public class Configuracoes
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public float VolumeMusica { get; set; }
    public float VolumeEfeitos { get; set; }
    public string Resolucao { get; set; }
    public int TelaCheia { get; set; } // 0 ou 1
    public int DefaultSave {  get; set; } // 0 ou 1
}

public class Progresso
{
    [PrimaryKey, AutoIncrement]
    public int IdSave { get; set; }
    public int ScoreRecord { get; set; }
    public int Coins { get; set; }
}

public class PowerUpsTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Name = "UX_SaveName", Order = 0, Unique = true)]
    public int IdSave { get; set; }
    [Indexed(Name = "UX_SaveName", Order = 1, Unique = true), Collation("NOCASE")]
    public string NamePower { get; set; }
    public int Nivel { get; set; }
    public float Duracao { get; set; } 
}
