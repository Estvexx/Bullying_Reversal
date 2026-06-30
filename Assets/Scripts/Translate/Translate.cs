using UnityEngine;

public static class Translation {
    public static string Get(string key) {
        string lang = PlayerPrefs.GetString(PlayerPrefsKeys.Language, "PT");

        return lang == "EN" ? GetEN(key) : GetPT(key);
    }

    private static string GetPT(string key) {
        return key switch {
            "play" => "Jogar",
            "settings" => "Definições",
            "record" => "Recorde",
            "lastScore" => "Últ. Pontuação",
            "exit" => "Sair",
            "music" => "Música",
            "sounds" => "Sons",
            "effects" => "Efeitos",
            "language" => "Idioma",
            "back" => "Voltar",
            "gameOver" => "Fim de Jogo",
            "score" => "Pontuação",
            "books" => "Livros",
            "portuguese" => "Português",
            "english" => "Inglês",
            _ => key
        };
    }

    private static string GetEN(string key) {
        return key switch {
            "play" => "Play",
            "settings" => "Settings",
            "record" => "Record",
            "lastScore" => "Last Score",
            "exit" => "Exit",
            "music" => "Music",
            "sounds" => "Sounds",
            "effects" => "Effects",
            "language" => "Language",
            "back" => "Back",
            "gameOver" => "Game Over",
            "score" => "Score",
            "books" => "Books",
            "portuguese" => "Portuguese",
            "english" => "English",
            _ => key
        };
    }
}
