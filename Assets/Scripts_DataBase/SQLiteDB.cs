using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class SQLiteDB : MonoBehaviour
{
    public static SQLiteDB instance;
    private string dbName = "DataBase.db";
    private SQLiteConnection _connection;

    [System.Serializable]
    public class Puntuacion
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int puntaje { get; set; }
        public DateTime fecha_hora { get; set; }
    }

    private void Awake()
    {
        instance = this;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // _connection = new SQLiteConnection(dbName, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        var ds = new DataService(dbName);
        _connection = ds.GetConnection();
        CreateTable();
    }

    private void CreateTable()
    {
        _connection.CreateTable<Puntuacion>();
    }

    public void GuardarPuntuacion(int puntuaje)
    {
        var nuevaPuntuacion = new Puntuacion
        {
            puntaje = puntuaje,
            fecha_hora = DateTime.Now
        };
        _connection.Insert(nuevaPuntuacion);
    }

    public int ObtenerPuntajeMasReciente()
    {
        var result = _connection.Table<Puntuacion>()
                          .OrderByDescending(p => p.fecha_hora)
                          .FirstOrDefault();

        return result?.puntaje ?? 0;
    }

    public int ObtenerMaximoPuntajeUltimas24Horas()
    {
        var fechaLimite = DateTime.Now.AddDays(-1);
        var result = _connection.Table<SQLiteDB.Puntuacion>()
                                .Where(p => p.fecha_hora >= fechaLimite)
                                .OrderByDescending(p => p.puntaje)
                                .FirstOrDefault();

        return result?.puntaje ?? 0;
    }

    public int ObtenerMaximoPuntajeUltimaSemana()
    {
        var fechaLimite = DateTime.Now.AddDays(-7);
        var result = _connection.Table<Puntuacion>()
                                .Where(p => p.fecha_hora >= fechaLimite)
                                .OrderByDescending(p => p.puntaje)
                                .FirstOrDefault();

        return result?.puntaje ?? 0;
    }

    public int ObtenerMaximoPuntaje()
    {
        var result = _connection.Table<Puntuacion>()
                            .OrderByDescending(p => p.puntaje)
                            .FirstOrDefault();

        return result?.puntaje ?? 0;
    }

    private void OnDestroy()
    {
        _connection?.Close();
    }
}