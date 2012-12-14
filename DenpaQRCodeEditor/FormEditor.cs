using System;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Media;
using System.Windows.Forms;
using DenpaQRCodeEditor.Properties;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;
using ErrorCorrectionLevel = com.google.zxing.qrcode.decoder.ErrorCorrectionLevel;

/*
 * Todolist:
 * 
 * TODO: add undo/redo
 * TODO: figure out the rest of the flags of the pushmo...
 * TODO: make drawing even faster by using draw-per-change instead of draw-the-all-thing-on-every-change
 */

namespace DenpaQRCodeEditor
{
    public partial class FormEditor : Form
    {
        private byte[] QRByteArray;
        private Bitmap qr_code;

        private enum denpa_type
        {
            not_valid = -1,
            north_america,
            japan,
            europe,
            forced_decode
        };

        private String[][] strStats = new String[49][];
        private int[][] intStats = new int[49][];

        private bool populating = false;

        #region denpa_men_enums
        enum AntennaPower
        {
            no_antenna = 0,
            recover_single,
            revive_single,
            cure_poison,
            cure_burn,
            cure_paralysis,
            cure_frozen,
            attack_fire_single,
            attack_water_single,
            attack_ice_single,
            attack_earth_single,
            attack_wind_single,
            attack_light_single,
            recover_all,
            revive_all,
            bonus_always_treasure,
            bonus_double_gold,
            buff_invincible,
            debuff_fatal,
            attack_fire_all,
            attack_water_all,
            attack_ice_all,
            attack_earth_all,
            attack_wind_all,
            attack_light_all,
            cure_sleep,
            cure_blind,
            buff_excitement,
            buff_attack_all,
            buff_defense_all,
            buff_speed_all,
            debuff_attack_all,
            buff_evasion_all,
            debuff_defense_all,
            debuff_speed_all,
            buff_attack_single,
            buff_defense_single,
            buff_speed_single,
            buff_evasion_single,
            debuff_poison,
            debuff_sleep,
            debuff_paralysis,
            debuff_blind,
            debuff_attack_single,
            debuff_defense_single,
            debuff_speed_single
        };
        #endregion
        #region denpa_men_resources
        Bitmap[] antenna = new Bitmap[46] {
            Resources.antenna_00,Resources.antenna_01,Resources.antenna_02,Resources.antenna_03,
            Resources.antenna_04,Resources.antenna_05,Resources.antenna_06,Resources.antenna_07,
            Resources.antenna_08,Resources.antenna_09,Resources.antenna_0A,Resources.antenna_0B,
            Resources.antenna_0C,Resources.antenna_0D,Resources.antenna_0E,Resources.antenna_0F,
            Resources.antenna_10,Resources.antenna_11,Resources.antenna_12,Resources.antenna_13,
            Resources.antenna_14,Resources.antenna_15,Resources.antenna_16,Resources.antenna_17,
            Resources.antenna_18,Resources.antenna_19,Resources.antenna_1A,Resources.antenna_1B,
            Resources.antenna_1C,Resources.antenna_1D,Resources.antenna_1E,Resources.antenna_1F,
            Resources.antenna_20,Resources.antenna_21,Resources.antenna_22,Resources.antenna_23,
            Resources.antenna_24,Resources.antenna_25,Resources.antenna_26,Resources.antenna_27,
            Resources.antenna_28,Resources.antenna_29,Resources.antenna_2A,Resources.antenna_2B,
            Resources.antenna_2C,Resources.antenna_2D,
        };

        Bitmap[] head = new Bitmap[24] { 
            Resources.head_0_0, Resources.head_0_1, Resources.head_0_2, Resources.head_0_3,
            Resources.head_0_4, Resources.head_0_5, Resources.head_0_6, Resources.head_0_7,
            Resources.head_0_8, Resources.head_0_9, Resources.head_0_A, Resources.head_1_0,
            Resources.head_1_1, Resources.head_1_2, Resources.head_1_3, Resources.head_1_4,
            Resources.head_1_5, Resources.head_1_6, Resources.head_2_0, Resources.head_2_1,
            Resources.head_2_2, Resources.head_2_3, Resources.head_2_4, Resources.head_2_5,
        };
        Bitmap[] faceshape = new Bitmap[32] {
            Resources.face_shape_0, Resources.face_shape_1, Resources.face_shape_2, Resources.face_shape_3,
            Resources.face_shape_4, Resources.face_shape_5, Resources.face_shape_6, Resources.face_shape_7,
            Resources.face_shape_8, Resources.hair_style_00,Resources.hair_style_01,Resources.hair_style_02,
            Resources.hair_style_03,Resources.hair_style_04,Resources.hair_style_05,Resources.hair_style_06,
            Resources.hair_style_07,Resources.hair_style_08,Resources.hair_style_09,Resources.hair_style_0A,
            Resources.hair_style_0B,Resources.hair_style_0C,Resources.hair_style_0D,Resources.hair_style_0E,
            Resources.hair_style_0F,Resources.hair_style_10,Resources.hair_style_11,Resources.hair_style_12,
            Resources.hair_style_13,Resources.hair_style_14,Resources.hair_style_15,Resources.hair_style_16
        };
        Bitmap[] haircolor = new Bitmap[12] {
            Resources.hair_color_0, Resources.hair_color_1, Resources.hair_color_2, Resources.hair_color_3,
            Resources.hair_color_4, Resources.hair_color_5, Resources.hair_color_6, Resources.hair_color_7,
            Resources.hair_color_8, Resources.hair_color_9, Resources.hair_color_A, Resources.hair_color_B
        };
        Bitmap[] cheek = new Bitmap[8] {
            Resources.cheek_0, Resources.cheek_1, Resources.cheek_2, Resources.cheek_3,
            Resources.cheek_4, Resources.cheek_5, Resources.cheek_6, Resources.cheek_7
        };
        Bitmap[] face_color = new Bitmap[6] {
            Resources.face_color_4, Resources.face_color_5, Resources.face_color_0,
            Resources.face_color_1, Resources.face_color_2, Resources.face_color_3
        };
        Bitmap[] eye_brow = new Bitmap[8] {
            Resources.eye_brow_0, Resources.eye_brow_1, Resources.eye_brow_2, Resources.eye_brow_3,
            Resources.eye_brow_4, Resources.eye_brow_5, Resources.eye_brow_6, Resources.eye_brow_7
        };
        Bitmap[] eyes = new Bitmap[32] {
            Resources.eyes_00, Resources.eyes_01, Resources.eyes_02, Resources.eyes_03,
            Resources.eyes_04, Resources.eyes_05, Resources.eyes_06, Resources.eyes_07,
            Resources.eyes_08, Resources.eyes_09, Resources.eyes_0A, Resources.eyes_0B,
            Resources.eyes_0C, Resources.eyes_0D, Resources.eyes_0E, Resources.eyes_0F,
            Resources.eyes_10, Resources.eyes_11, Resources.eyes_12, Resources.eyes_13,
            Resources.eyes_14, Resources.eyes_15, Resources.eyes_16, Resources.eyes_17,
            Resources.eyes_18, Resources.eyes_19, Resources.eyes_1A, Resources.eyes_1B,
            Resources.eyes_1C, Resources.eyes_1D, Resources.eyes_1E, Resources.eyes_1F
        };
        Bitmap[] glasses = new Bitmap[16] {
            Resources.glasses_0, Resources.glasses_1, Resources.glasses_2, Resources.glasses_3,
            Resources.glasses_4, Resources.glasses_5, Resources.glasses_6, Resources.glasses_7,
            Resources.glasses_8, Resources.glasses_9, Resources.glasses_A, Resources.glasses_B,
            Resources.glasses_C, Resources.glasses_D, Resources.glasses_E, Resources.glasses_F
        };
        Bitmap[] mouth = new Bitmap[32] {
            Resources.mouth_00, Resources.mouth_01, Resources.mouth_02, Resources.mouth_03,
            Resources.mouth_04, Resources.mouth_05, Resources.mouth_06, Resources.mouth_07,
            Resources.mouth_08, Resources.mouth_09, Resources.mouth_0A, Resources.mouth_0B,
            Resources.mouth_0C, Resources.mouth_0D, Resources.mouth_0E, Resources.mouth_0F,
            Resources.mouth_10, Resources.mouth_11, Resources.mouth_12, Resources.mouth_13,
            Resources.mouth_14, Resources.mouth_15, Resources.mouth_16, Resources.mouth_17,
            Resources.mouth_18, Resources.mouth_19, Resources.mouth_1A, Resources.mouth_1B,
            Resources.mouth_1C, Resources.mouth_1D, Resources.mouth_1E, Resources.mouth_1F,
        };
        Bitmap[] nose = new Bitmap[16] {
            Resources.nose_0, Resources.nose_1, Resources.nose_2, Resources.nose_3,
            Resources.nose_4, Resources.nose_5, Resources.nose_6, Resources.nose_7,
            Resources.nose_8, Resources.nose_9, Resources.nose_A, Resources.nose_B,
            Resources.nose_C, Resources.nose_D, Resources.nose_E, Resources.nose_F
        };

        String[] north_america_names = new String[] {
            #region north_america_names
                "Aaden", "Aaron", "Abdiel", "Abel", "Ace", "Adan", "Adonis", "Adrian", "Aedan", 
                "Agustin", "Ahmad", "Ahmed", "Al", "Alden", "Aldo", "Alessandro", "Alicius", 
                "Alijah", "Allan", "Allen", "Alonso", "Alonzo", "Alton", "Amare", "Amari", 
                "Amir", "Anderson", "Andres", "Andrew", "Andy", "Angel", "Angelo", "Antoine", 
                "Antonio", "Antwan", "Archer", "Ari", "Aric", "Ariel", "Armani", "Arnav", 
                "Aron", "Aryan", "Asa", "Ashton", "Atticus", "August", "Austin", "Avery", 
                "Ayaan", "Bailey", "Backham", "Beau", "Benito", "Bennett", "Benson", "Bentlee", 
                "Bently", "Bill", "Blaze", "Bo", "Bobby", "Bort", "Boston", "Braden", 
                "Bradley", "Bradyn", "Brantley", "Braxton", "Brayan", "Braydon", "Brendon", 
                "Brice", "Brock", "Brodie", "Bronn", "Brooks", "Bruce", "Bruno", "Bryan", 
                "Bryslen", "Burt", "Byron", "Cade", "Caden", "Cain", "Cale", "Caleb", "Calen", 
                "Calvin", "Camden", "Camdyn", "Camilo", "Cameron", "Cannon", "Carl", "Carson", 
                "Casen", "Cash", "Cedric", "Chace", "Chaim", "Chance", "Charles", "Charlie", 
                "Chase", "Chester", "Chet", "Chi", "Clayton", "Cody", "Coleman", "Colin", 
                "Collin", "Colten", "Connor", "Conrad", "Cooper", "Corbin", "Cortez", "Cory", 
                "Craig", "Cristian", "Christopher", "Crew", "Cristofer", "Cristopher", 
                "Crystal", "Cyrus", "Dakota", "Dale", "Dalton", "Damarion", "Damian", "Damon", 
                "Dangelo", "Daniel", "Dante", "Darnell", "Dashawn", "Davion", "Davis", 
                "Dayton", "Deangelo", "Deon", "Deshawn", "Desmond", "Destin", "Diego", 
                "Dillan", "Don", "Donald", "Donovan", "Donte", "Dorian", "Dorsey", "Douglas", 
                "Drake", "Draven", "Drew", "Duncan", "Dyluck", "Eden", "Edison", "Efrain", 
                "Efran", "Elias", "Eliseo", "Elisha", "Elvis", "Emmett", "Enzo", "Ernesto", 
                "Ethan", "Evan", "Everett", "Fabian", "Felipe", "Felix", "Felton", "Fernando", 
                "Finn", "Finnegan", "Fisher", "Forest", "Francis", "Frederick", "Fredrick", 
                "Gage", "Garland", "Garrett", "Gary", "Gavin", "Geoffrey", "Gerard", "Gianni", 
                "Gilbert", "Gilliam", "Giovanni", "Giuseppe", "Glenn", "Grady", "Gregory", 
                "Greyson", "Guillermo", "Gunnar", "Gunner", "Gus", "Gustavo", "Haiden", "Hank", 
                "Harley", "Harold", "Harper", "Harrison", "Hassan", "Hayes", "Heath", 
                "Hezekiah", "Holden", "Hollis", "Homer", "Horacio", "Houston", "Hudson", 
                "Hugh", "Hugo", "Hunter", "Ian", "Ibrahim", "Iker", "Irvin", "Irving", 
                "Isaiah", "Ishaan", "Isiah", "Ivan", "Izaiah", "Jabari", "Jackson", "Jaden", 
                "Jadiel", "Jadyn", "Jae", "Jagger", "Jairo", "Jake", "Jakob", "Jamal", "Jamar", 
                "Jamari", "Jamarion", "Jamel", "James", "Jameson", "Jamie", "Jaquan", "Jared", 
                "Jase", "Jasiah", "Jason", "Jasper", "Jaxon", "Jay", "Jayden", "Jaydon", 
                "Jaylin", "Jean", "Jensen", "Jeremiah", "Jermaine", "Jerry", "Jett", "Joaquin", 
                "Joe", "Joel", "Joey", "Jonah", "Jorah", "Jordan", "Jose", "Joseph", "Joshua", 
                "Josiah", "Josue", "Jovanni", "Joziah", "Judah", "Jude", "Julien", "Julius", 
                "Justice", "Justin", "Justus", "Kai", "Kamari", "Kamden", "Kameron", "Kane", 
                "Karter", "Kayden", "Keagan", "Keaton", "Keith", "Kelvin", "Keon", "Keshawn", 
                "Khalil", "Kieran", "Killian", "King", "Kingston", "Kody", "Kolby", "Kole", 
                "Kolton", "Kristian", "Kyler", "Kymani", "Lamar", "Lance", "Landon", "Lane", 
                "Lathan", "Layton", "Legend", "Leigh", "Len", "Lennon", "Lenny", "Leo", "Leon", 
                "Leonard", "Leonardo", "Leonidas", "Leroy", "Lionel", "Louis", "Lucas", 
                "Lucian", "Luis", "Lyric", "Malachi", "Malik", "Manuel", "Marc", "Marcos", 
                "Mario", "Mark", "Markus", "Marley", "Marquis", "Marvin", "Mason", "Mathew", 
                "Matthew", "Mathias", "Matteo", "Mauricio", "Maverick", "Maxim", "Maximo", 
                "Maximiliano", "Maxwell", "Melvin", "Memphis", "Micheal", "Mike", "Mikey", 
                "Miles", "Milo", "Misael", "Monroe", "Montgomery", "Nash", "Nasir", "Nathan", 
                "Neal", "Nestor", "Nicholas", "Nickolas", "Nico", "Nikolas", "Noah", "Noe", 
                "Nolan", "Norman", "Octavio", "Oliver", "Omar", "Pablo", "Parker", "Paul", 
                "Payton", "Pedro", "Phil", "Phillip", "Phoenix", "Quentin", "Rafael", "Ralph", 
                "Ramiro", "Ramon", "Rashad", "Raul", "Ray", "Rayan", "Raymond", "Reed", 
                "Reese", "Reggie", "Reid", "Remy", "Reyes", "Rhett", "Ricardo", "Rick", 
                "Rickon", "Ricky", "Robert", "Roberto", "Rock", "Roderick", "Rodney", 
                "Rodrigo", "Rohan", "Roman", "Ronaldo", "Ronan", "Ronin", "Ronnie", "Ross", 
                "Rowan", "Roy", "Royce", "Ruben", "Rubin", "Rudy", "Russell", "Ryder", "Ryker", 
                "Ryland", "Rylee", "Sage", "Sal", "Sam", "Samir", "Samson", "Samuel", 
                "Santiago", "Santino", "Saul", "Sawyer", "Sean", "Seymour", "Shane", "Shawn", 
                "Sheldon", "Sid", "Sidney", "Silas", "Sincere", "Slade", "Stan", "Stanley", 
                "Stephan", "Stephen", "Sullivan", "Syrio", "Talan", "Tanner", "Tatum", "Teddy", 
                "Terence", "Terrance", "Terrell", "Terrence", "Theodore", "Tim", "Timmy", 
                "Timothy", "Tobias", "Toby", "Trent", "Trenton", "Tristan", "Tyler", "Tyree", 
                "Tyrone", "Ulises", "Uriel", "Valentino", "Vern", "Vernon", "Victor", 
                "Vincent", "Wade", "Walder", "Waldorf", "Wesley", "Winston", "Xander", 
                "Xavier", "Yael", "Yair", "Yandel", "York", "Zack", "Zackarv", "Zachary", 
                "Zaid", "Zaire", "Zander", "Zayne", "Zion"
                #endregion
        };

        String[] jap_names = new String[] {
            #region japanese_names
                "あかね", "あかり", "あきとら", "あさひ", "あおと", "あると", "あずま", "いお", "いかん", 
                "いきる", "いく", "いくお", "いくし", "いくま", "いくまさ", "いくむ", "いさじ", "いざむ", 
                "いちか", "いちき", "いちご", "いちせい", "いちた", "いちだい", "いちと", "いちは", "いちひろ",
                "いちや", "いちよう", "いちる", "いちろ", "いちろうた", "いつお", "いつひと", "いとし", 
                "いっこう", "いっさ", "いっせい", "いぶみ", "いわお", "いった", "うきょう", "うこん", "うしお", 
                "うじやす", "うちゅう", "うてな", "うみたろう", "うみのすけ", "うみや", "うめたろう", "うめじろう", 
                "うゆう", "うめた", "うりゅう", "うん", "えいき", "えいきち", "えいご", "えいし", "えいじ", 
                "えいしゅん", "えいす", "えいすけ", "えいだい", "えいたろう", "えいのすけ", "えいま", "えいや", 
                "えつお", "えつろう", "えにし", "えびぞう", "えんぞう", "おういち", "おういちろう", "おうが", 
                "おうし", "おうしろう", "おうじ", "おうじろう", "おうや", "おおぞら", "おおや", "おと", "おとき", 
                "おとや", "おりと", "かいお", "かいが", "かいき", "かいこう", "かいし", "かいじ", "かいと", 
                "がいと", "かいへい", "かいや", "かおる", "がくた", "がくや", "かげと", "かげひろ", "かざと", 
                "かずお", "かずおみ", "かずき", "かずさ", "かずし", "かずしげ", "かずたか", "かずたけ", 
                "かずただ", "かずちか", "かずと", "かずとら", "かずのすけ", "かずは", "かずひさ", "かずひと", 
                "かずひろ", "かずふみ", "かずまさ", "かずみつ", "かずむ", "かずや", "かずやす", "かつ", 
                "かつあき", "かつお", "かつじ", "かつと", "かつなり", "かつひこ", "かつひと", "かつら", 
                "かなた", "かなと", "かなで", "かなや", "がもん", "かをる", "かんいち", "かんくろう", "がんじ", 
                "かんすけ", "かんぞう", "かんたろう", "ぎいちろう", "きおのすけ", "きくたろう", "きさぶろう", 
                "きしん", "きっぺい", "きひと", "きひろ", "きみのぶ", "きょうが", "きょうき", "きょうじろう", 
                "きょうすけ", "ぎょうすけ", "きょうたろう", "きょうと", "きょうま", "きよし", "きよしげ", "きよすみ", 
                "きよた", "きよと", "きよなり", "きよのり", "きよのぶ", "きよひと", "きよひら", "きよふみ", 
                "きよま", "きりや", "ぎんが", "きんじろう", "ぎんじろう", "きんたろう", "ぎんと", "きんぺい", 
                "くうご", "くうと", "くすお", "くにとりまる", "くにのり", "くにひこ", "くにひで", "くにひと", 
                "くまたろう", "くらのすけ", "くらま", "くろうど", "くんた", "けい", "けいいちろう", "けいえつ", 
                "けいか", "けいじ", "けいじゅ", "けいしん", "けいすけ", "けいた", "けいだい", "けいん", 
                "けんいちすけ", "けんいちろう", "けんき", "げんき", "けんさく", "けんしろう", "けんじ", "げんじ", 
                "けんすけ", "げんすけ", "けんせい", "けんた", "けんたすけ", "けんたひ", "けんたひこ", 
                "げんたろう", "げんと", "げんのすけ", "けんま", "げんき", "こいちろう", "こう", "こういち", 
                "こういちろう", "こうえい", "こうえつ", "こうお", "こうし", "こうじ", "ごうすけ", "こうた", 
                "ごうだい", "ごうたろう", "こうふ", "こうま", "こうめい", "こうよう", "こうりゅう", "ごくう", "こころ", 
                "こじろう", "コタロー", "こてつ", "ことや", "ごろう", "ごんべえ", "さいき", "さいた", "さいと", 
                "さかえ", "さくいち", "さくお", "さくじ", "さくた", "さくたろう", "さくはる", "さくへい", "さくも", 
                "さくや", "さくら", "さだむ", "さだゆき", "さちお", "さちと", "さちひろ", "さちや", "さとゆき", 
                "さとる", "さねひと", "さのすけ", "さへい", "さまのしん", "さわお", "しあら", "しいち", 
                "じいちろう", "しおり", "しげお", "しげかず", "しげき", "しげなり", "しげひこ", "しげひさ", 
                "しげひろ", "しげまつ", "しこう", "しさお", "じじろう", "しずお", "しのすけ", "しのび", "しのぶ", 
                "しひろ", "しゅう", "しゅうう", "しゅうが", "しゅうげつ", "しゅうご", "じゅうたろう", "しゅうのすけ", 
                "しゅうへい", "しゅうや", "しゅうよ", "しゅうわ", "じゅり", "しゅんいち", "じゅんきち", "じゅんすけ", 
                "じゅんせい", "しゅんた", "じゅんのすけ", "しゅんぺい", "しゅんま", "じゅんま", "しょう", 
                "しょうせい", "しょうた", "しょうだい", "じょうた", "しょうのすけ", "しょうりゅう", "しりゅう", "しろと", 
                "しろう", "じろう", "しんいち", "じんいち", "しんご", "しんざぶろう", "しんじ", "じんぺい", 
                "しんめい", "しんや", "すい", "すいた", "すいと", "すけろく", "すずし", "すばる", "すみと", 
                "すみひこ", "すみはる", "せいあ", "せいご", "せいざぶろう", "せいじ", "せいじろう", "せいしん", 
                "せいすけ", "せいたろう", "せいと", "せいよう", "せいら", "せが", "せつお", "せつや", "せな", 
                "せんいちろう", "せんと", "せんま", "せんご", "せいすけ", "せつお", "そういちろう", "そうが", 
                "そうけん", "そうご", "そうし", "そうじろう", "そうせき", "そうた", "そうだい", "そうたろう", 
                "そうと", "そうま", "そうや", "そらお", "そらき", "そらじろう", "そらや", "たいかい", "だいき", 
                "だいごろう", "だいさく", "たいじゅ", "だいしょう", "だいじろう", "だいすけ", "たいち", "たいと", 
                "だいと", "だいもん", "だいや", "たかいち", "たかお", "たかおみ", "たかつぐ", "たかと", 
                "たかとし", "たかな", "たかね", "たかのぶ", "たかひこ", "たかひさ", "たかひろ", "たかま", 
                "たかみち", "たかみつ", "たかむ", "たかゆき　", "たきお　", "たくい", "たくや", "たくし", 
                "たくすけ", "たくた", "たくたろう", "たくのすけ", "たくふみ", "たくむ", "たくろう", "たけお", 
                "たけき", "たけし", "たけじろう", "たけとも", "たけのぶ", "たけひさ", "たけひと", "たけふみ", 
                "たけまつ", "たけみつ", "たけゆき", "たける", "たすけ", "ただあき", "ただずみ", "ただたか", 
                "ただのぶ", "ただむね", "たつ", "たつおみ", "たつし", "たっと", "たつなり", "たつひこ", 
                "たつひさ", "たつひと", "たっぺい", "たつま", "たつや", "たつよし", "たへい", "たみや", 
                "たみひで", "たもつ", "たろう", "ちあき", "ちおん", "ちかと", "ちかひさ", "ちかゆき", "ちせい", 
                "ちづる", "ちづお", "ちとせ", "ちひろ", "ちゅうすけ", "ちょういち", "ちょうた", "つかさ", 
                "つづみ", "つね", "つねのり", "つねひと", "つねゆき", "つねろう", "つばき", "つばさ", 
                "ていた", "ていと", "てつ", "てつき", "てっせい", "てつた", "てつひさ", "てつひで", "てつひと", 
                "てつま", "テル", "てるあき", "てるお", "てるかず", "てると", "てるとし", "てるのり", "てるま", 
                "てるまさ", "てるみ", "てるみち", "てるよし", "てん", "てんいち", "てんさく", "でんじ", 
                "てんせい", "てんたろう", "てんと", "てんどう", "とうじろう", "とうた", "とうたろう", "とうま", 
                "とうや", "とおる", "ときつぐ", "ときじろう", "ときと", "ときひさ", "ときむね", "ときや", "とくひろ", 
                "としいえ", "としたか", "としなり", "としのぶ", "としまさ", "としみち", "としやす", "としゆき", 
                "とま", "とみお", "とみなり", "とみひさ", "ともかず", "ともき", "ともし", "ともたろう", "ともちか", 
                "ともと", "とものすけ", "とものり", "ともひで", "ともひと", "ともみつ", "ともゆき", "ともよし", 
                "ともろう", "とよき", "とらきち", "とらじろう", "とらひと", "とらお", "なおえ", "なおかず", 
                "なおざね", "なおじろう", "なおた", "なおたか", "なおたろう", "なおとし", "なおはる", 
                "なおひこ", "なおひろ", "なおま", "ながひさ", "なぎと", "なごみ", "なごむ", "なつ", 
                "なついち", "なつお", "なつた", "なつたろう", "なつひと", "なつめ", "ななき", "ななせ", 
                "ななみ", "なゆた", "なり", "なりたつ", "なりひろ", "なりゆき", "なるひと", "なるみ", 
                "にじひこ", "にじや", "にじろう", "にや", "にんざぶろう", "ねおん", "のあ", "ノエル", "のびた", 
                "のぶお", "のぶき", "のぶたか", "のぶてる", "のぶなが", "のぶひろ", "のりかず", "のりたか", 
                "のりひと", "のりゆき", "のりまさ", "はいとく", "はかる", "ばく", "はじめ", "はちろう", "はつき", 
                "はづき", "はつね", "はつま", "ははや", "はやき", "はやたか", "はやと", "はやとし", 
                "はやとも", "はやぶさ", "はゆま", "はゆる", "はるあき", "はるお", "はるか", "はるかず", 
                "はるきよ", "はるすけ", "はるたけ", "はるただ", "はるたろう", "はるとも", "はるとら", "はるなお", 
                "はるなり", "はるね", "はるのり", "はるひ", "はるひこ", "はるひで", "はるま", "はるみち", 
                "はるむ", "はんた", "ばんたろう", "ばんり", "ひかり", "ひかる", "ひこの", "ひさ", "ひさあき", 
                "ひさお", "ひさたか", "ひさのり", "ひさのぶ", "ひさひろ", "ひさや", "ひさよし", "ひじり", 
                "ピッド", "ひづき", "ひさはろ", "ひで", "ひでちか", "ひでとし", "ひでとも", "ひでなり", 
                "ひでひさ", "ひでゆき", "ひでよし", "ひなき", "ひなた", "ひなと", "ひのき", "ひゅう", "ひょう", 
                "ひょうえ", "ひょうが", "ひらお", "ひろかつ", "ひろさき", "ひろき", "ひろこ", "ひろたろう", 
                "ひろひこ", "ひろひで", "ひろまさ", "ひろみつ", "びん", "ふうすけ", "ふうたろう", "ふうと", 
                "ふきち", "ふく", "ふくすけ", "ふくと", "ふくや", "ふじお", "ふたば", "ぶどう", "ふみたけ", 
                "ふみひさ", "ふみひと", "ふゆ", "ふゆと", "ぶんじ", "ぶんたろう", "ぶんや", "べんぞう", 
                "へいた", "へんたい", "ポール", "ほずき", "ほづみ", "ほまれ", "ほうせい", "ほの", "マイケル", 
                "まいじろう", "まいたろう", "まお", "まきた", "まこ", "まさ", "まさおみ", "まさかつ", "まさかど", 
                "まさき", "まさじ", "まさしげ", "まさた", "まこと", "まさたか", "まさただ", "まさちか", "まさと", 
                "まさひろ", "まさほ", "まさむね", "まさみ", "まさみつ", "まさゆき", "まさゆみ", "まさよし", 
                "まさる", "ますお", "またさぶろう", "またざぶろう", "まつたろう", "まどか", "まなつ", "まなと", 
                "まなぶ", "まはる", "まほ", "まみち", "まもる", "まゆき", "まるも", "まれすけ", "まんえい", 
                "まんじろう", "まんぺい", "みお", "みおと", "みかど", "みき", "みきたろう", "みきたか", "みく", 
                "みくや", "みこと", "みさきまる", "みずと", "みずほ", "みずや", "みち", "みちお", "みちき", 
                "みちた", "みちと", "みちなり", "みちはる", "みちひこ", "みちひと", "みちまさ", "みちる", 
                "みちろう", "みつあき", "みつお", "みつき", "みつくに", "みつたか", "みつと", "みつなが", 
                "みつはる", "みつひこ", "みつひさ", "みつひで", "みつまさ", "みつろう", "みどう", "みどり", 
                "みのや", "みのる", "みやび", "みゆき", "みわたくろう", "むが", "むくなが", "むつお", 
                "むつや", "むねお", "むさし", "むねじ", "めいとく", "めかのすけ", "めぐむ", "メグウィン", 
                "もとき", "もぎき", "もときよ", "もとのり", "もとのぶ", "もとひろ", "モンきち", "もんきゅー", 
                "やいち", "やいちろう", "やくも", "やすあき", "やすお", "やすさぶろう", "やすし", "やすとし", 
                "やすなり", "やすひこ", "やすひで", "やすひと", "やすひろ", "やすま", "やすまさ", "やすや", 
                "やすろう", "やひこ", "やまと", "やわら", "ゆいいちろう", "ゆいのすけ", "ゆいと", "ゆいや", 
                "ゆう", "ゆうあ", "ゆうあき", "ゆういち", "ゆうさ", "ゆうさく", "ゆうざぶろう", "ゆうじゅ", 
                "ゆうしょう", "ゆうしん", "ゆうせい", "ゆうだい", "ゆうほ", "ゆうま", "ゆうや", "ゆかり", 
                "ゆきお", "ゆきち", "ゆきとも", "ゆきとら", "ゆきのしん", "ゆきのぶ", "ゆきひと", "ゆきま", 
                "ゆきのすけ", "ゆずる", "ゆみと", "ゆめ", "ゆめき", "ゆめじ", "ゆめた", "ゆらい", "ユウナ", 
                "よういち", "ようく", "ようたろう", "ようと", "よこたる", "よしいえ", "よしお", "よしき", "よしくに", 
                "よした", "よしたろう", "よしちか", "よしつぐ", "よしなお", "よしひこ", "よしひろ", "よしまさ", 
                "よしみつ", "よみ", "よりひと", "ヨン", "らいじ", "らいち", "らいと", "らいな", "ライヤ", "らん", 
                "らんた", "らんぽ", "らんのすけ", "ライト", "りおと", "りきあ", "りきいち", "りきお", "りきと", 
                "りく", "りくたろう", "りくと", "りくのすけ", "りくひろ", "りくへい", "りくみ", "りくや", "りたろう", 
                "りつき", "りつし", "りつじ", "りつたろう", "りつと", "りっと　", "りつま", "りと", "りゅう", 
                "りゅういち", "りゅうし", "りゅうすけ", "りゅうじゅ", "りゅうた", "りゅうたろう", "りゅうだい", 
                "りゅうのすけ", "りゅうほ", "りゅうほう", "りょうい", "りょうえい", "りょうすけ", "りょうた", 
                "りょうたろう", "りょうのしん", "りょうのすけ", "りょうご", "りょうこう", "りょうじろう", "りょうふう", 
                "りょうま", "りょうきち", "りょお", "りんいち", "りんいちろう", "りんご", "りんぞう", "りんた", 
                "りんや", "りくや", "りつき", "りくみ", "ルイス", "るうま", "るおん", "るか", "るきあ", "るな", 
                "るり", "るろ", "れいいち", "れいき", "れいし", "れいのすけ", "れいや", "れいめい", "レイン", 
                "れお", "れき", "レン", "れんいちろう", "れんじ", "れんじゅ", "えいき", "えいき", "えいき", 
                "れんじゅ", "れんじろう", "れんたろう", "れんぺい", "れんま", "れんや", "レレレのおじさん", 
                "ろうた", "ろく", "ろくろう", "ろまん", "ろん", "わいち", "わかき", "わかぎ", "わかさぎ", 
                "わかと", "わかな", "わかひろ", "わく", "わしひと", "わりと"
            #endregion
        };

        #endregion

        public String[] AddItemToArray(String[] original, String itemToAdd)
        {

            String[] finalArray = new String[original.Length + 1];
            for (int i = 0; i < original.Length; i++)
            {
                finalArray[i] = original[i];
            }

            finalArray[finalArray.Length - 1] = itemToAdd;

            return finalArray;
        }

        public void update_names()
        {
            StreamReader name;
            String line;
            bool inside_area;
            if (File.Exists("jap_names.txt"))
            {
                jap_names = new String[0];
                name = new StreamReader("jap_names.txt");
                inside_area = false;
                while ((line = name.ReadLine()) != null)
                {
                    if (line == "#areaedit")
                        inside_area = true;
                    else if (line == "#areaedit(end)")
                        inside_area = false;
                    else
                    {
                        if (inside_area)
                        {
                            line = line.Replace("（主人公）", "");
                            line = line.Replace("(主人公)", "");
                            line = line.Replace("(公式サイトのＱＲコードにて掲載)", "");
                            if (line != "")
                                jap_names = AddItemToArray(jap_names, line);
                        }
                    }
                }
                name.Close();
            }
            if (File.Exists("na_names.txt"))
            {
                north_america_names = new String[0];
                name = new StreamReader("na_names.txt");
                inside_area = false;
                while ((line = name.ReadLine()) != null)
                {
                    if (line == "%")
                        inside_area = true;
                    else if (line == "")
                        inside_area = false;
                    else
                    {
                        if (inside_area)
                        {
                            line = line.Replace("'''''", "");
                            line = line.Replace(" (hero)", "");
                            line = line.Replace(" (hero's fiance)", "");
                            north_america_names = AddItemToArray(north_america_names, line);
                        }
                    }
                }
                name.Close();
            }
        }

        Byte[][] Denpa_regions = new Byte[][] {
            new Byte[] { (Byte)'A',0,(Byte)'h',0,(Byte)'4',0,(Byte)'3',0},
            new Byte[] { (Byte)'b',0,(Byte)'X',0,(Byte)'8',0,(Byte)'0',0},
            new Byte[] { (Byte)'j',0,(Byte)'3',0,(Byte)'Z',0,(Byte)'w',0},
        };

        private bool character_stats_initialized = false;
        private void init_character_stats()
        {
            if (character_stats_initialized) return;
            #region character_stats
            strStats[(int)AntennaPower.no_antenna] = new String[27] {
                "HP:29 AP:0 Att:11 Def:13 Spd:3 Ev:0",
                "HP:32 AP:0 Att:12 Def:13 Spd:3 Ev:0",
                "HP:35 AP:0 Att:12 Def:14 Spd:3 Ev:0",
                "HP:37 AP:0 Att:12 Def:15 Spd:2 Ev:0",
                "HP:40 AP:0 Att:13 Def:16 Spd:2 Ev:0",
                "HP:24 AP:0 Att:10 Def:12 Spd:3 Ev:3",
                "HP:27 AP:0 Att:10 Def:13 Spd:3 Ev:3",
                "HP:29 AP:0 Att:10 Def:13 Spd:3 Ev:3",
                "HP:32 AP:0 Att:10 Def:13 Spd:3 Ev:3",
                "HP:35 AP:0 Att:11 Def:14 Spd:2 Ev:3",
                "HP:35 AP:0 Att:10 Def:15 Spd:3 Ev:3",
                "HP:37 AP:0 Att:11 Def:15 Spd:2 Ev:3",
                "HP:21 AP:0 Att:9 Def:11 Spd:3 Ev:6",
                "HP:24 AP:0 Att:9 Def:12 Spd:3 Ev:6",
                "HP:27 AP:0 Att:9 Def:12 Spd:3 Ev:6",
                "HP:27 AP:0 Att:10 Def:13 Spd:3 Ev:6",
                "HP:27 AP:0 Att:9 Def:13 Spd:2 Ev:6",
                "HP:27 AP:0 Att:9 Def:11 Spd:3 Ev:6",
                "HP:29 AP:0 Att:10 Def:13 Spd:2 Ev:6",
                "HP:29 AP:0 Att:9 Def:13 Spd:2 Ev:6",
                "HP:32 AP:0 Att:10 Def:14 Spd:2 Ev:6",
                "HP:18 AP:0 Att:9 Def:11 Spd:3 Ev:10",
                "HP:24 AP:0 Att:9 Def:12 Spd:3 Ev:10",
                "HP:27 AP:0 Att:9 Def:12 Spd:3 Ev:10",
                "HP:16 AP:0 Att:8 Def:10 Spd:3 Ev:16",
                "HP:18 AP:0 Att:8 Def:11 Spd:3 Ev:16",
                "HP:21 AP:0 Att:8 Def:11 Spd:3 Ev:16"
            };
            intStats[(int)AntennaPower.no_antenna] = new int[] { 
                384, 320, 321, 322, 385, 325, 3, 0, 1, 2, 
                326, 324, 9, 10, 6, 7, 11, 327, 8, 330, 
                328, 329, 333, 387, 386, 331, 332,
                12,14,4,
                4,0,
                5,1,
                323,3
            };

            strStats[(int)AntennaPower.revive_single] = new String[52] {
                "HP:25 AP:"," Att:9 Def:9 Spd:4 Ev:0",
                "HP:27 AP:"," Att:10 Def:9 Spd:4 Ev:0",
                "HP:29 AP:"," Att:10 Def:9 Spd:4 Ev:0",
                "HP:32 AP:"," Att:10 Def:10 Spd:3 Ev:0",
                "HP:34 AP:"," Att:11 Def:11 Spd:3 Ev:0",
                "HP:20 AP:"," Att:9 Def:8 Spd:4 Ev:3",
                "HP:23 AP:"," Att:9 Def:9 Spd:4 Ev:3",
                "HP:25 AP:"," Att:9 Def:9 Spd:4 Ev:3",
                "HP:27 AP:"," Att:9 Def:9 Spd:4 Ev:3",
                "HP:29 AP:"," Att:9 Def:9 Spd:3 Ev:3",
                "HP:29 AP:"," Att:9 Def:10 Spd:4 Ev:3",
                "HP:32 AP:"," Att:9 Def:10 Spd:3 Ev:3",
                "HP:18 AP:"," Att:8 Def:8 Spd:4 Ev:6",
                "HP:20 AP:"," Att:8 Def:8 Spd:4 Ev:6",
                "HP:23 AP:"," Att:8 Def:9 Spd:3 Ev:6",
                "HP:23 AP:"," Att:8 Def:8 Spd:4 Ev:6",
                "HP:23 AP:"," Att:9 Def:9 Spd:4 Ev:6",
                "HP:25 AP:"," Att:9 Def:9 Spd:3 Ev:6",
                "HP:25 AP:"," Att:8 Def:9 Spd:3 Ev:6",
                "HP:27 AP:"," Att:9 Def:9 Spd:3 Ev:6",
                "HP:16 AP:"," Att:8 Def:7 Spd:4 Ev:10",
                "HP:20 AP:"," Att:8 Def:8 Spd:4 Ev:10",
                "HP:23 AP:"," Att:8 Def:8 Spd:4 Ev:10",
                "HP:13 AP:"," Att:7 Def:7 Spd:4 Ev:16",
                "HP:16 AP:"," Att:7 Def:7 Spd:4 Ev:16",
                "HP:18 AP:"," Att:7 Def:8 Spd:4 Ev:16"
            };
            intStats[(int)AntennaPower.revive_single] = new int[] { 384, 320, 321, 322, 385, 325, 
                3, 0, 1, 2, 326, 324, 9, 10, 11, 6, 7, 8, 330, 328, 
                329, 333, 387, 386, 331, 332,
                12,14,4,
                4,0,
                5,1,
                323,3,
                327,6,
            };

            strStats[(int)AntennaPower.attack_water_all] = new String[54] { 
                "HP:17 AP:"," Att:9 Def:12 Spd:3 Ev:0",
                "HP:19 AP:"," Att:10 Def:12 Spd:3 Ev:0",
                "HP:20 AP:"," Att:10 Def:13 Spd:3 Ev:0",
                "HP:22 AP:"," Att:10 Def:14 Spd:2 Ev:0",
                "HP:24 AP:"," Att:11 Def:15 Spd:2 Ev:0",
                "HP:14 AP:"," Att:9 Def:11 Spd:3 Ev:3",
                "HP:16 AP:"," Att:9 Def:12 Spd:3 Ev:3",
                "HP:17 AP:"," Att:9 Def:12 Spd:3 Ev:3",
                "HP:19 AP:"," Att:9 Def:12 Spd:3 Ev:3",
                "HP:20 AP:"," Att:9 Def:13 Spd:2 Ev:3",
                "HP:20 AP:"," Att:9 Def:14 Spd:3 Ev:3",
                "HP:22 AP:"," Att:9 Def:14 Spd:2 Ev:3",
                "HP:12 AP:"," Att:8 Def:10 Spd:3 Ev:6",
                "HP:14 AP:"," Att:8 Def:11 Spd:3 Ev:6",
                "HP:16 AP:"," Att:8 Def:11 Spd:3 Ev:6",
                "HP:16 AP:"," Att:9 Def:12 Spd:3 Ev:6",
                "HP:16 AP:"," Att:8 Def:12 Spd:2 Ev:6",
                "HP:16 AP:"," Att:8 Def:10 Spd:3 Ev:6",
                "HP:17 AP:"," Att:9 Def:12 Spd:2 Ev:6",
                "HP:17 AP:"," Att:8 Def:12 Spd:2 Ev:6",
                "HP:19 AP:"," Att:9 Def:13 Spd:2 Ev:6",
                "HP:11 AP:"," Att:8 Def:10 Spd:3 Ev:10",
                "HP:14 AP:"," Att:8 Def:11 Spd:3 Ev:10",
                "HP:16 AP:"," Att:8 Def:11 Spd:3 Ev:10",
                "HP:9 AP:"," Att:7 Def:9 Spd:3 Ev:16",
                "HP:11 AP:"," Att:7 Def:10 Spd:3 Ev:16",
                "HP:12 AP:"," Att:7 Def:10 Spd:3 Ev:16"
            };
            intStats[(int)AntennaPower.attack_water_all] = new int[] { 
                384, 320, 321, 322, 385, 325, 3, 0, 
                1, 2, 326, 324, 9, 10, 6, 7, 
                11, 327, 8, 330, 328, 329, 333, 387, 
                386, 331, 332,
                12,14,4,
                4,0,
                5,1,
                323,3,
            };
            #endregion
            character_stats_initialized = true;
        }

        public FormEditor()
        {
            InitializeComponent();
            menuFileNew_Click(null, null);
            update_names();
        }
        
        #region Menu -> File

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            var data = new Byte[0x6A];
            QRByteArray = data;
            if (hexBox1.ByteProvider != null)
            {
                IDisposable byteProvider = hexBox1.ByteProvider as IDisposable;
                if (byteProvider != null)
                    byteProvider.Dispose();
                hexBox1.ByteProvider = null;
            }
            hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(QRByteArray);
            populating = false;
            cboColor.SelectedIndex = -1;
            cboColor_SelectedIndexChanged(null, null);
            
            cboHeadShape.SelectedIndex = -1;
            cboHeadShape_SelectedIndexChanged(null, null);
            cboFaceShapeHairStyle.SelectedIndex = -1;
            cboFaceShapeHairStyle_SelectedIndexChanged(null, null);
            cboHairColor.SelectedIndex = -1;
            cboHairColor_SelectedIndexChanged(null, null);
            cboEyes.SelectedIndex = -1;
            cboEyes_SelectedIndexChanged(null, null);
            cboEyeBrows.SelectedIndex = -1;
            cboEyeBrows_SelectedIndexChanged(null, null);
            cboNose.SelectedIndex = -1;
            cboNose_SelectedIndexChanged(null, null);
            cboFaceColor.SelectedIndex = -1;
            cboFaceColor_SelectedIndexChanged(null, null);
            cboMouth.SelectedIndex = -1;
            cboMouth_SelectedIndexChanged(null, null);
            cboCheeks.SelectedIndex = -1;
            cboCheeks_SelectedIndexChanged(null, null);
            cboGlasses.SelectedIndex = -1;
            cboGlasses_SelectedIndexChanged(null, null);
            cboAntennaPower.SelectedIndex = -1;
            cboAntennaPower_SelectedIndexChanged(null, null);
            nudStats.Value = -1;
            nudStats_ValueChanged(null, null);
            txtName.Text = "New Denpa";    //Cannot leave the name field in the QR data totally blank or else the game locks up instantly.
            txtName_TextChanged(null, null);

            switch (CultureInfo.CurrentCulture.Name)
            {
                case "":
                case "en":
                case "fr":
                case "es":
                    cboRegion.SelectedIndex = -1;
                    cboRegion_SelectedIndexChanged(null, null);
                    break;

                case "ja":
                case "ja-JP":
                    cboRegion.SelectedIndex = 1;
                    cboRegion_SelectedIndexChanged(null, null);
                    break;

                case "en-CA":
                case "en-US":
                case "fr-CA":
                case "es-MX":
                case "es-AR":
                case "en-BZ":
                case "es-BO":
                case "pt-BR":
                case "es-CL":
                case "es-CO":
                case "es-CR":
                case "es-DO":
                case "es-EC":
                case "es-SV":
                case "es-GT":
                case "es-HN":
                case "en-JM":
                case "ms-MY":
                case "es-NI":
                case "es-PA":
                case "es-PY":
                case "es-PE":
                case "es-SA":
                case "zh-SG":
                case "es-TT":
                case "es-UY":
                case "es-VE":
                    cboRegion.SelectedIndex = 0;
                    cboRegion_SelectedIndexChanged(null, null);
                    break;

                default:
                    cboRegion.SelectedIndex = 2;
                    cboRegion_SelectedIndexChanged(null, null);
                    break;
            }

            btnChangeID_Click(sender, e);
            StatusStripLabel.Text = "";
            Byte[] bytearray = new byte[0x6A];
            for (int i = 0; i < 0x6A; i++)
                bytearray[i] = hexBox1.ByteProvider.ReadByte(i);
            load_QR_data(bytearray);
            
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Menu -> QR Code

        private ByteMatrix GetQRMatrix(int size)
        {
            var writer = new QRCodeWriter();
            const string encoding = "ISO-8859-1";
            if (hexBox1.ByteProvider == null)
                return null;
            if (hexBox1.ByteProvider.Length == 0)
                return null;
            QRByteArray = new Byte[0];
            var data = new Byte[0];
            for (int i = 0; i < hexBox1.ByteProvider.Length; i++)
            {
                data = new Byte[QRByteArray.Length + 1];
                QRByteArray.CopyTo(data, 0);
                data[i] = hexBox1.ByteProvider.ReadByte(i);
                QRByteArray = data;
            }

            if (!advancedInterfaceToolStripMenuItem.Checked || !dontDecryptToolStripMenuItem.Checked)
                data = Crypto.Encrypt(QRByteArray,true);
            else
                data = QRByteArray;

            if (data == null)
                return null;
            var str = Encoding.GetEncoding(encoding).GetString(data);
            var hints = new Hashtable { { EncodeHintType.CHARACTER_SET, encoding }, {EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.Q } };
            return writer.encode(str, BarcodeFormat.QR_CODE, size, size, hints); 
        }

        #endregion


        #region Check for updates
        private String _remoteVer;
        private bool _checkNow;
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _remoteVer = @"<Error: Couldn't parse the version number>";
                var request = (HttpWebRequest)WebRequest.Create("http://denpa-qr-code-editor.googlecode.com/git/DenpaQRCodeEditor/Properties/AssemblyInfo.cs");
                var responseStream = request.GetResponse().GetResponseStream();
                if (responseStream == null) return;
                var reader = new StreamReader(responseStream);
                string line;
                while ((line = reader.ReadLine()) != null)
                    if (line.Contains("AssemblyFileVersion")) //Get the version between the quotation marks
                    {
                        var start = line.IndexOf('"') + 1;
                        var len = line.LastIndexOf('"') - start;
                        _remoteVer = line.Substring(start, len);
                        break;
                    }
            }
            catch
            {
                //No harm done...possibly no internet connection
            }
        }

        private bool IsNewerAvailable(string newerVersion)
        {
            var thisVersion = Version.Parse(Application.ProductVersion);
            var remoteVersion = Version.Parse(newerVersion);
            return remoteVersion.CompareTo(thisVersion) > 0;
        }

        private void bwCheckForUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsNewerAvailable(_remoteVer))
                MessageBox.Show(string.Format(@"This version is v{0}" + Environment.NewLine + "The version on the server is v{1}" + Environment.NewLine + "You might want to download a newer version.", Application.ProductVersion, _remoteVer));
            else if (_checkNow)
                MessageBox.Show(string.Format(@"v{0} is the latest version.", Application.ProductVersion));
        }
        #endregion

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {

        }

        private void hexBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var matrix = GetQRMatrix(100);
                if (matrix == null)
                    return;
                var img = new Bitmap(200, 200);
                var g = Graphics.FromImage(img);
                g.Clear(Color.White);
                for (var y = 0; y < matrix.Height; ++y)
                    for (var x = 0; x < matrix.Width; ++x)
                        if (matrix.get_Renamed(x, y) != -1)
                            g.FillRectangle(Brushes.Black, x * 2, y * 2, 2, 2);
                if (btnSwitchPicBox.Text == "Feature")
                    picBox.Image = img;
                else
                    picBox.Image = picBox2.Image;
                qr_code = img;
                if (!dontDecryptToolStripMenuItem.Checked)
                {
                    numericUpDown1.Minimum = 0;
                    numericUpDown1.Maximum = hexBox1.ByteProvider.Length;
                }
                else
                {
                    numericUpDown1.Minimum = 0x10;
                    numericUpDown1.Maximum = 0x32;
                }
                numericUpDown1_ValueChanged(null, null);
            }
            catch
            {
            }
        }

        private void hexBox1_KeyUp(object sender, KeyEventArgs e)
        {
            hexBox1_KeyPress(sender,null);
        }

        private void saveQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            var imgFormat = ImageFormat.Png;
            switch (Path.GetExtension(sfd.FileName))
            {
                case ".jpg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    imgFormat = ImageFormat.Png;
                    break;
                case ".bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;
                case ".gif":
                    imgFormat = ImageFormat.Gif;
                    break;
            }
            try
            {
                qr_code.Save(sfd.FileName, imgFormat);
            }
            catch
            {
            }
        }

        private int IsQRDenpa(Byte[] bytearray)
        {
            Byte[] byteArray = new Byte[0x6A];
            if ((dontDecryptToolStripMenuItem.Checked == true))
                return (int)denpa_type.forced_decode;   //If the menu is hidden in the checked state, then it is no longer possible
            //to determine the header bytes, except on a 1/ 2^32 chance.  However, size checking
            //is still possible.
            else if (bytearray.Length == 0x6A)
            {
                Array.Copy(bytearray, byteArray, 0x6A);
                if (!advancedInterfaceToolStripMenuItem.Checked || !dontDecryptToolStripMenuItem.Checked)
                    byteArray = Crypto.Decrypt(byteArray);
                else
                    return (int)denpa_type.forced_decode;

                for(int i = 0; i < Denpa_regions.Length; i++)
                {
                    bool is_denpa = true;
                    for(int j=0;(j<8)&&is_denpa;j++)
                        if(byteArray[j] != Denpa_regions[i][j])
                            is_denpa = false;
                    if(is_denpa)
                        return i;
                }
       
                if ((advancedInterfaceToolStripMenuItem.Checked == true))
                    return (int)denpa_type.forced_decode;
            }
            else if ((advancedInterfaceToolStripMenuItem.Checked == true))
                return (int)denpa_type.forced_decode;

            return (int)denpa_type.not_valid;
        }

        private void openQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "All Supported|*.png;*.jpg;*.bmp;*.gif|PNG Files|*.png|Jpeg Files|*.jpg|Bitmap Files|*.bmp|GIF Files|*.gif" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                var file = new FileStream(ofd.FileName, FileMode.Open);
                var bmp = new Bitmap(Image.FromStream(file));
                file.Close();
                var binary = new BinaryBitmap(new HybridBinarizer(new RGBLuminanceSource(bmp, bmp.Width, bmp.Height)));
                var reader = new QRCodeReader();
                var hashtable = new Hashtable();
                hashtable.Add(DecodeHintType.POSSIBLE_FORMATS,BarcodeFormat.QR_CODE);
                hashtable.Add(DecodeHintType.TRY_HARDER,true);
                var result = reader.decode(binary, hashtable);
                var byteArray = (byte[])((ArrayList)result.ResultMetadata[ResultMetadataType.BYTE_SEGMENTS])[0];
                load_QR_data(byteArray);
            }
            catch (ReaderException ex)
            {
                MessageBox.Show("Error Loading:" + Environment.NewLine + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error Loading:" + Environment.NewLine + ex.Message);
            }
        }

        private void color_pic_box(int color1, int color2)
        {
            picBox3.Image = new Bitmap(100, 100);
            var g = Graphics.FromImage(picBox3.Image);
            g.Clear(Color.White);
            g.FillRectangle(Brushes.Black, 0, 0, 2, 100);
            g.FillRectangle(Brushes.Black, 0, 0, 100, 2);
            g.FillRectangle(Brushes.Black, 98, 0, 2, 100);
            g.FillRectangle(Brushes.Black, 0, 98, 100, 2);
            for (var y = 2; y < 98; ++y)
            {
                for (var x = 2; x < 50; ++x)
                {
                    //g.FillRectangle(Brushes.Black, x * 2, y * 2, 2, 2);
                    switch (color1)
                    {
                        default:
                        case 0:
                            g.FillRectangle(Brushes.Black, x, y, 1, 1);
                            break;
                        case 1:
                            g.FillRectangle(Brushes.White, x, y, 1, 1);
                            break;
                        case 2:
                            g.FillRectangle(Brushes.Red, x, y, 1, 1);
                            break;
                        case 3:
                            g.FillRectangle(Brushes.Blue, x, y, 1, 1);
                            break;
                        case 4:
                            g.FillRectangle(Brushes.Cyan, x, y, 1, 1);
                            break;
                        case 5:
                            g.FillRectangle(Brushes.Orange, x, y, 1, 1);
                            break;
                        case 6:
                            g.FillRectangle(Brushes.Green, x, y, 1, 1);
                            break;
                    }
                    switch (color2)
                    {
                        default:
                        case 0:
                            g.FillRectangle(Brushes.Black, x+48, y, 1, 1);
                            break;
                        case 1:
                            g.FillRectangle(Brushes.White, x+48, y, 1, 1);
                            break;
                        case 2:
                            g.FillRectangle(Brushes.Red, x+48, y, 1, 1);
                            break;
                        case 3:
                            g.FillRectangle(Brushes.Blue, x+48, y, 1, 1);
                            break;
                        case 4:
                            g.FillRectangle(Brushes.Cyan, x+48, y, 1, 1);
                            break;
                        case 5:
                            g.FillRectangle(Brushes.Orange, x+48, y, 1, 1);
                            break;
                        case 6:
                            g.FillRectangle(Brushes.Green, x+48, y, 1, 1);
                            break;
                    }
                    
                }
            }
        }

        private void cboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int color_index = cboColor.SelectedIndex;
            if (color_index == -1) return;
            int[] picboxcolors = new int[] {
                0,0, 1,1, 2,2, 3,3, 4,4, 5,5, 6,6, 
                2,3, 2,4, 2,5, 2,6, 2,1,
                3,4, 3,5, 3,6, 3,1,
                4,5, 4,6, 4,1,
                5,6, 5,1,
                6,1
            };
            color_pic_box(picboxcolors[color_index * 2], picboxcolors[(color_index * 2) + 1]);
            if (populating) return;
            bool solid_color_enable;
            int antenna_index = cboAntennaPower.SelectedIndex;
      
            if (antenna_index == -1) { StatusStripLabel.Text = "Antenna Power required"; return; }
            if ((color_index <= 6))
            {
                if ((((antenna_index >= 7) && (antenna_index <= 12)) || ((antenna_index >= 19) && (antenna_index <= 24))))
                {
                    int[][] color_index_table = new int[6][];
                    color_index_table[0] = new int[7] { 6, 1, 0, 2, 3, 4, 5 };
                    color_index_table[1] = new int[7] { 6, 1, 2, 0, 3, 4, 5 };
                    color_index_table[2] = new int[7] { 6, 1, 2, 3, 0, 4, 5 };
                    color_index_table[3] = new int[7] { 6, 1, 2, 3, 4, 0, 5 };
                    color_index_table[4] = new int[7] { 6, 1, 2, 3, 4, 5, 0 };
                    color_index_table[5] = new int[7] { 6, 0, 1, 2, 3, 4, 5 };
                    if (antenna_index <= 12)
                        antenna_index -= 7;
                    else
                        antenna_index -= 19;
                    color_index = color_index_table[antenna_index][color_index];
                }
                solid_color_enable = (color_index != 0);

                write_value(denpa_data.color_1, (color_index - ((solid_color_enable) ? 1 : 0)));
                write_value(denpa_data.color_2, (solid_color_enable) ? 0x28 : 0x00);
            }
            else
            {
                write_value(denpa_data.color_1, cboColor.SelectedIndex - 7);
                write_value(denpa_data.color_2, 0x5F);
            }
        }

        private void update_stats()
        {
            String[] AP_head_shape = new String[25] {
                "N/A",
                "7","7","7","7","7","7","7",
                "12","12","12","12",
                "15","15","15","15","15","15","15",
                "20","20","20","24","20","24"
            };
            init_character_stats();
            int index = cboAntennaPower.SelectedIndex;
            if ((int)nudStats.Value < 0)
                return;
            
            if (strStats[index] != null)
            {
                populating = true;
                int stats = (int)nudStats.Value & 0x1F;
                int stats2 = (int)nudStats.Value >> 5;
                int range = (index == 0) ? (strStats[index].Length) : (strStats[index].Length / 2);

                if ((stats2 >= 0x0A) && (stats2 <= 0x0B))
                {
                    stats %= intStats[index][range + 1];
                    stats += 320;
                }
                else if (stats2 == 0x0C)
                {
                    stats %= intStats[index][range + 2];
                    stats += 384;
                }
                else
                    stats %= intStats[index][range + 0];

                cboStats.Items.Clear();
                
                if (index == 0)
                    cboStats.Items.AddRange(strStats[index]);
                else
                {
                    for (int i = 0; i < range; i++)
                        cboStats.Items.Add(strStats[index][(i * 2)] + AP_head_shape[cboHeadShape.SelectedIndex + 1] + strStats[index][(i * 2) + 1]);
                }
                for (int i = 0; i < range; i++)
                {
                    if ((int)stats == intStats[index][i])
                    {
                        cboStats.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = range + 3; i < intStats[index].Length; i += 2)
                {
                    if ((int)stats == intStats[index][i])
                    {
                        for (int j = 0; j < range; j++)
                        {
                            if (intStats[index][i+1] == intStats[index][j])
                            {
                                cboStats.SelectedIndex = j;
                                break;
                            }
                        }
                        break;
                    }
                }
                populating = false;
            }
            else
            {
                cboStats.Items.Clear();
            }
        }

        private void cboAntennaPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboColor_SelectedIndexChanged(sender, e);  //Colors depend on Antenna power
            StatusStripLabel.Text = ""; 
            int index = cboAntennaPower.SelectedIndex;
            if (index == -1) return;
            picBox2.Image = antenna[index];
            if (populating) return;
            update_stats();
            

            if ((index >= 1) && (index <= 12))
            {
                write_value(denpa_data.antenna_1, index - 1);
                write_value(denpa_data.antenna_2, 0x46);
            }
            else if ((index >= 13) && (index <= 24))
            {
                write_value(denpa_data.antenna_1, index - 13);
                write_value(denpa_data.antenna_2, 0x5A);
            }
            else if (index >= 25)
            {
                write_value(denpa_data.antenna_1, index - 25);
                write_value(denpa_data.antenna_2, 0x5D);
            }
            else
            {
                write_value(denpa_data.antenna_1, 0);
                write_value(denpa_data.antenna_2, 0);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (populating) return;
            if (txtName.TextLength > 0)
            {
                byte[] unicode_str = System.Text.Encoding.Unicode.GetBytes(txtName.Text);
                for (int i = 0; i < 48; i++)
                    hexBox1.ByteProvider.WriteByte(0x34 + i, 0);
                for (int i = 0; i < txtName.TextLength; i++)
                {
                    hexBox1.ByteProvider.WriteByte(0x34 + (i*4), unicode_str[(i*2)]);
                    hexBox1.ByteProvider.WriteByte(0x36 + (i * 4), unicode_str[(i*2)+1]);
                }
                hexBox1.Refresh();
                hexBox1_KeyPress(null, null);
            }
        }

        private void btnSwitchPicBox_Click(object sender, EventArgs e)
        {
            if (btnSwitchPicBox.Text == "QR Code")
            {
                picBox.Image = qr_code;
                btnSwitchPicBox.Text = "Feature";
            }
            else
            {
                picBox.Image = picBox2.Image;
                btnSwitchPicBox.Text = "QR Code";
            }
        }

        private void cboHeadShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboHeadShape.SelectedIndex;
            int AntennaIndex = cboAntennaPower.SelectedIndex;
            if (index == -1) return;
            picBox4.Image = head[index];
            if (populating) return;
            update_stats();

            if ((index >= 0) && (index <= 10))
            {
                write_value(denpa_data.head_shape_1, index);
                write_value(denpa_data.head_shape_2, 0);
            }
            else if ((index >= 11) && (index <= 17))
            {
                write_value(denpa_data.head_shape_1, index-11);
                write_value(denpa_data.head_shape_2, 0x50);
            }
            else
            {
                write_value(denpa_data.head_shape_1, index-18);
                write_value(denpa_data.head_shape_2, 0x5F);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void newDenpaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFileNew_Click(sender, e);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuFileExit_Click(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

      
        private void btnChangeID_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            write_value(0x0C, 0, 16, random.Next(0, 65535));
            write_value(0x64, 0, 24, random.Next(0, 16777215));
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboFaceShapeHairStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFaceShapeHairStyle.SelectedIndex;
            if (index == -1) return;
            picBox5.Image = faceshape[index];
            if (populating) return;
            if (index < 9)
            {
                write_value(denpa_data.face_shape_1, index);
                write_value(denpa_data.face_shape_2, 0);
            }
            else
            {
                write_value(denpa_data.face_shape_1, index - 9);
                write_value(denpa_data.face_shape_2, 0x5A);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboHairColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboHairColor.SelectedIndex;
            if (index == -1) return;
            picBox6.Image = haircolor[index];
            if (populating) return;
            
            write_value(denpa_data.hair_color, index);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboEyes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboEyes.SelectedIndex;
            if (index == -1) return;
            picBox8.Image = eyes[index];
            if (populating) return;
            
            write_value(denpa_data.eyes, index);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboEyeBrows_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboEyeBrows.SelectedIndex;
            if (index == -1) return;
            picBox9.Image = eye_brow[index];
            if (populating) return;
            
            write_value(denpa_data.eyebrows, index);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboNose_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboNose.SelectedIndex;
            if (index == -1) return;
            picBox12.Image = nose[index];
            if (populating) return;
            
            write_value(denpa_data.nose, index);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboFaceColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFaceColor.SelectedIndex;
            if (index == -1) return;
            picBox7.Image = face_color[index];
            if (populating) return;
            
            if (index < 2)
            {
                write_value(denpa_data.face_color_1, index);
                write_value(denpa_data.face_color_2, 0x0C);
            }
            else
            {
                write_value(denpa_data.face_color_1, index-2);
                write_value(denpa_data.face_color_2, 0x00);
            }
            hexBox1.Refresh();
            
            hexBox1_KeyPress(null, null);
        }

        private void cboMouth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboMouth.SelectedIndex;
            if (index == -1) return;
            picBox10.Image = mouth[index];
            if (populating) return;

            write_value(denpa_data.mouth, index);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboCheeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboCheeks.SelectedIndex;
            if (index == -1) return;
            picBox13.Image = cheek[index];
            if (populating) return;
            
            if (index < 1)
            {
                write_value(denpa_data.cheeks_1, index);
                write_value(denpa_data.cheeks_2, 0);
            }
            else
            {
                write_value(denpa_data.cheeks_1, index - 1);
                write_value(denpa_data.cheeks_2, 0x5A);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboGlasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboGlasses.SelectedIndex;
            if (index == -1) return;
            picBox11.Image = glasses[index];
            if (populating) return;

            if (index < 1)
            {
                write_value(denpa_data.glasses_1, index);
                write_value(denpa_data.glasses_2, 0);
            }
            else
            {
                write_value(denpa_data.glasses_1, index-1);
                write_value(denpa_data.glasses_2, 0x2D);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void nudStats_ValueChanged(object sender, EventArgs e)
        {
            if (populating) return;
            int index = (int)nudStats.Value;
            if (index == -2) index = 415;
            if (index == 319) index = 31;
            if (index == 383) index = 351;
            if (index == 32) index = 320;
            if (index == 352) index = 384;
            if (index == 416) index = 0;
            nudStats.Value = index;
            if (index < 0) return;
            update_stats();

            write_value(denpa_data.stats_1, index);
            write_value(denpa_data.stats_2, index >> 5);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void btnRandomDenpa_Click(object sender, EventArgs e)
        {
            btnChangeID_Click(sender, e);
            Random random = new Random();
            cboAntennaPower.SelectedIndex = random.Next(cboAntennaPower.Items.Count);
            cboColor.SelectedIndex = random.Next(cboColor.Items.Count);
            cboHeadShape.SelectedIndex = random.Next(cboHeadShape.Items.Count);
            cboFaceShapeHairStyle.SelectedIndex = random.Next(cboFaceShapeHairStyle.Items.Count);
            cboHairColor.SelectedIndex = random.Next(cboHairColor.Items.Count);
            cboEyes.SelectedIndex = random.Next(cboEyes.Items.Count);
            cboEyeBrows.SelectedIndex = random.Next(cboEyeBrows.Items.Count);
            cboNose.SelectedIndex = random.Next(cboNose.Items.Count);
            cboFaceColor.SelectedIndex = random.Next(cboFaceColor.Items.Count);
            cboMouth.SelectedIndex = random.Next(cboMouth.Items.Count);
            cboCheeks.SelectedIndex = random.Next(cboCheeks.Items.Count);
            cboGlasses.SelectedIndex = random.Next(cboGlasses.Items.Count);
            nudStats.Value = random.Next(512);
            if (cboRegion.SelectedIndex != 1)
                txtName.Text = north_america_names[random.Next(north_america_names.Length)];
            else
                txtName.Text = jap_names[random.Next(jap_names.Length)];
        }

        private void advancedInterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            btnSwitchPicBox.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            dontDecryptToolStripMenuItem.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            numericUpDown1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            numericUpDown2.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            numericUpDown3.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            label18.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            label17.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            label16.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            maskedTextBox1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            button1.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            numericUpDown5.Visible = !advancedInterfaceToolStripMenuItem.Checked;
            advancedInterfaceToolStripMenuItem.Checked = !advancedInterfaceToolStripMenuItem.Checked;
            

            if (!advancedInterfaceToolStripMenuItem.Checked)
                if (btnSwitchPicBox.Text == "QR Code")
                    btnSwitchPicBox_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (populating) return;
            if(cboRegion.SelectedIndex != -1)
                for (int i = 0; i < 8; i++)
                    hexBox1.ByteProvider.WriteByte(i, Denpa_regions[cboRegion.SelectedIndex][i]);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (populating) return;
            int stat_index = cboStats.SelectedIndex;
            int antenna_index = cboAntennaPower.SelectedIndex;
            if (stat_index == -1) return;
            if (antenna_index == -1) { StatusStripLabel.Text = "Antenna Power required"; return; }
            if (intStats[antenna_index] == null) return;
            nudStats.Value = intStats[antenna_index][stat_index];
        }

        private void populate_combo_boxes(Byte[] byteArray)
        {
            populating = true;
            if (byteArray == null)
            {
                cboColor.SelectedIndex = -1;
                cboColor_SelectedIndexChanged(null, null);

                cboHeadShape.SelectedIndex = -1;
                cboHeadShape_SelectedIndexChanged(null, null);
                cboFaceShapeHairStyle.SelectedIndex = -1;
                cboFaceShapeHairStyle_SelectedIndexChanged(null, null);
                cboHairColor.SelectedIndex = -1;
                cboHairColor_SelectedIndexChanged(null, null);
                cboEyes.SelectedIndex = -1;
                cboEyes_SelectedIndexChanged(null, null);
                cboEyeBrows.SelectedIndex = -1;
                cboEyeBrows_SelectedIndexChanged(null, null);
                cboNose.SelectedIndex = -1;
                cboNose_SelectedIndexChanged(null, null);
                cboFaceColor.SelectedIndex = -1;
                cboFaceColor_SelectedIndexChanged(null, null);
                cboMouth.SelectedIndex = -1;
                cboMouth_SelectedIndexChanged(null, null);
                cboCheeks.SelectedIndex = -1;
                cboCheeks_SelectedIndexChanged(null, null);
                cboGlasses.SelectedIndex = -1;
                cboGlasses_SelectedIndexChanged(null, null);
                cboAntennaPower.SelectedIndex = -1;
                cboAntennaPower_SelectedIndexChanged(null, null);
                nudStats.Value = -1;
                nudStats_ValueChanged(null, null);
                cboRegion.SelectedIndex = -1;
                cboRegion_SelectedIndexChanged(null, null);
                txtName.Text = "";    //Cannot leave the name field in the QR data totally blank or else the game locks up instantly.
                txtName_TextChanged(null, null);
                Application.DoEvents();
                populating = false;
                return;
            }


            Byte[] name = new Byte[24];
            if ((IsQRDenpa(byteArray) != (int)denpa_type.not_valid)&&(IsQRDenpa(byteArray) != (int)denpa_type.forced_decode))
            {
                cboRegion.SelectedIndex = IsQRDenpa(byteArray);
                for (int i = 0; i < 24; i++)
                {
                    name[i] = byteArray[0x34 + (i * 2)];
                }
                txtName.Text = System.Text.Encoding.Unicode.GetString(name);
                cboEyes.SelectedIndex = read_value(denpa_data.eyes);
                cboEyeBrows.SelectedIndex = read_value(denpa_data.eyebrows);
                if ((read_value(denpa_data.glasses_2) >= 0x2D) && (read_value(denpa_data.glasses_2) <= 0x31))
                    cboGlasses.SelectedIndex = (read_value(denpa_data.glasses_1) % 15) + 1;
                else
                    cboGlasses.SelectedIndex = 0;
                int Antenna1 = read_value(denpa_data.antenna_1);
                int Antenna2 = read_value(denpa_data.antenna_2);
                int shift_color = -1;
                if ((Antenna2 >= 0x46) && (Antenna2 <= 0x59))
                {
                    Antenna1 %= 12;
                    if (Antenna1 >= 6)
                        shift_color = Antenna1 - 6;
                    cboAntennaPower.SelectedIndex = Antenna1 + 1;
                }
                else if ((Antenna2 > 0x59) && (Antenna2 <= 0x5C))
                {
                    Antenna1 %= 12;
                    if (Antenna1 >= 6)
                        shift_color = Antenna1 - 6;
                    cboAntennaPower.SelectedIndex = Antenna1 + 1 + 12;
                }
                else if ((Antenna2 > 0x5C) && (Antenna2 <= 0x63))
                {
                    Antenna1 %= 21;
                    cboAntennaPower.SelectedIndex = Antenna1 + 1 + 12 + 12;
                }
                else
                {
                    cboAntennaPower.SelectedIndex = 0;
                }

                int hairstyle = read_value(denpa_data.face_shape_1);
                int hairstyle2 = read_value(denpa_data.face_shape_2);
                if ((hairstyle2 >= 0x5A) && (hairstyle2 <= 0x63))
                    cboFaceShapeHairStyle.SelectedIndex = (hairstyle % 23) + 9;
                else
                    cboFaceShapeHairStyle.SelectedIndex = (hairstyle % 9);
                cboHairColor.SelectedIndex = read_value(denpa_data.hair_color) % 12;

                int color1 = read_value(denpa_data.color_1);
                int color2 = read_value(denpa_data.color_2);

                if ((color2 >= 0x5F) && (color2 <= 0x63))
                    cboColor.SelectedIndex = (color1 % 15) + 7;
                else
                {
                    if (shift_color > -1)
                    {
                        int[][] color_index_table = new int[6][];
                        color_index_table[0] = new int[7] { 1, 3, 4, 5, 6, 0, 1 };
                        color_index_table[1] = new int[7] { 1, 2, 4, 5, 6, 0, 1 };
                        color_index_table[2] = new int[7] { 1, 2, 3, 5, 6, 0, 1 };
                        color_index_table[3] = new int[7] { 1, 2, 3, 4, 6, 0, 1 };
                        color_index_table[4] = new int[7] { 1, 2, 3, 4, 5, 0, 1 };
                        color_index_table[5] = new int[7] { 2, 3, 4, 5, 6, 0, 1 };

                        int[] default_color = new int[6] { 2, 3, 4, 5, 6, 1 };
                        if ((color2 >= 0x28) && (color2 <= 0x5E))
                        {
                            cboColor.SelectedIndex = color_index_table[shift_color][(color1 % 7)];
                        }
                        else
                        {
                            cboColor.SelectedIndex = default_color[shift_color];
                        }
                    }
                    else
                    {
                        if ((color2 >= 0x28) && (color2 <= 0x5E))
                        {
                            cboColor.SelectedIndex = (color1 % 7) + 1;
                        }
                        else
                        {
                            cboColor.SelectedIndex = 0;
                        }
                    }
                }

                nudStats.Value = read_value(denpa_data.stats_1) | (read_value(denpa_data.stats_2) << 5);
                cboMouth.SelectedIndex = read_value(denpa_data.mouth) % 32;
                cboNose.SelectedIndex = read_value(denpa_data.nose) % 16;

                int facecolor1 = read_value(denpa_data.face_color_1);
                int facecolor2 = read_value(denpa_data.face_color_2);
                if ((facecolor2 >= 0x0C) && (facecolor2 <= 0x18))
                    cboFaceColor.SelectedIndex = facecolor1 % 2;
                else
                    cboFaceColor.SelectedIndex = (facecolor1 % 4) + 2;

                int cheeks1 = read_value(denpa_data.cheeks_1);
                int cheeks2 = read_value(denpa_data.cheeks_2);
                if ((cheeks2 >= 0x5A) && (cheeks2 <= 0x63))
                    cboCheeks.SelectedIndex = (cheeks1 % 7) + 1;
                else
                    cboCheeks.SelectedIndex = 0;

                int headshape1 = read_value(denpa_data.head_shape_1);
                int headshape2 = read_value(denpa_data.head_shape_2);

                if ((headshape2 >= 0x50) && (headshape2 <= 0x5E))
                    cboHeadShape.SelectedIndex = (headshape1 % 7) + 11;
                else if ((headshape2 >= 0x5F) && (headshape2 <= 0x63))
                    cboHeadShape.SelectedIndex = (headshape1 % 6) + 11 + 7;
                else
                    cboHeadShape.SelectedIndex = (headshape1 % 11);

            }
            Application.DoEvents();
            populating = false;
            update_stats();
        }

        private void load_QR_data(Byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (!advancedInterfaceToolStripMenuItem.Checked || !dontDecryptToolStripMenuItem.Checked)
                    QRByteArray = Crypto.Decrypt(byteArray);
                else
                    QRByteArray = byteArray;
                if (!advancedInterfaceToolStripMenuItem.Checked || !dontDecryptToolStripMenuItem.Checked)
                    if (IsQRDenpa(QRByteArray) == (int)denpa_type.not_valid)
                    {
                        MessageBox.Show("Error: Scanned QR code is not recognized as a Denpa men QR code");
                        return;
                    }
                if (hexBox1.ByteProvider != null)
                {
                    IDisposable byteProvider = hexBox1.ByteProvider as IDisposable;
                    if (byteProvider != null)
                        byteProvider.Dispose();
                    hexBox1.ByteProvider = null;
                }
                hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(QRByteArray);
                hexBox1_KeyPress(null, null);


                var temp = new byte[0];
                temp = new Byte[QRByteArray.Length];
                QRByteArray.CopyTo(temp, 0);    //Make sure any tampering with the combo boxes do NOT mess with this data.
                if (!advancedInterfaceToolStripMenuItem.Checked || !dontDecryptToolStripMenuItem.Checked)
                    populate_combo_boxes(Crypto.Decrypt(temp));
                else
                    populate_combo_boxes(null);
            }
        }

        private void captureQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load_QR_data(CameraCapture.GetByteArray(true));
        }

        private void dontDecryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dontDecryptToolStripMenuItem.Checked = !dontDecryptToolStripMenuItem.Checked;
            if (!dontDecryptToolStripMenuItem.Checked)
            {
                numericUpDown1.Minimum = 0;
                numericUpDown1.Maximum = hexBox1.ByteProvider.Length;
            }
            else
            {
                numericUpDown1.Minimum = 0x10;
                numericUpDown1.Maximum = 0x32;
            }
        }

        private void checkForNewVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _checkNow = true;
            bwCheckForUpdates.RunWorkerAsync();
        }

        enum denpa_data
        {
            antenna_1 = 0,
            antenna_2,
            stats_1,
            stats_2,
            color_1,
            color_2,
            head_shape_1,
            head_shape_2,
            face_shape_1,
            face_shape_2,
            face_color_1,
            face_color_2,
            hair_color,
            eyes,
            nose,
            mouth,
            eyebrows,
            cheeks_1,
            cheeks_2,
            glasses_1,
            glasses_2,
            unknown_0,
            unknown_1,
            unknown_2,
            unknown_3,
            unknown_4,
            unknown_5,
            unknown_6,
            unknown_7,
            unknown_8,
            unknown_9,
            unknown_A
        };

        int[][] offsets = new int[][] {
            new int[] { 0x10, 0, 6 },   //Antenna power
            new int[] { 0x26, 6, 7 },   //Antenna power class
            new int[] { 0x10, 6, 5 },   //Stats
            new int[] { 0x22, 0, 4 },   //Stat class
            new int[] { 0x12, 3, 5 },   //Color
            new int[] { 0x24, 0, 7 },   //Color class
            new int[] { 0x14, 0, 5 },   //Head shape
            new int[] { 0x24, 7, 7 },   //Head shape class
            new int[] { 0x14, 5, 6 },   //Face shape/hair style
            new int[] { 0x28, 5, 7 },   //Faces shape/hair style class
            new int[] { 0x16, 3, 2 },   //Face color
            new int[] { 0x30, 0, 5 },   //Face color class
            new int[] { 0x18, 0, 5 },   //Hair color
            new int[] { 0x18, 5, 5 },   //Eyes
            new int[] { 0x1A, 3, 4 },   //Nose
            new int[] { 0x1C, 0, 6 },   //Mouth
            new int[] { 0x1C, 6, 3 },   //Eyebrows
            new int[] { 0x1E, 3, 5 },   //Cheeks
            new int[] { 0x2C, 0, 7 },   //Cheek class
            new int[] { 0x20, 0, 5 },   //Glasses
            new int[] { 0x2E, 0, 6 },   //Glasses class
            
            new int[] { 0x16, 5, 3 },   //Unknown purpose from here on in. :)
            new int[] { 0x1A, 2, 1 },
            new int[] { 0x1A, 7, 1 },
            new int[] { 0x1E, 1, 2 },
            new int[] { 0x20, 5, 3 },
            new int[] { 0x22, 4, 4 },
            new int[] { 0x2A, 4, 4 },
            new int[] { 0x2C, 7, 1 },
            new int[] { 0x2E, 6, 2 },
            new int[] { 0x30, 5, 3 },
            new int[] { 0x32, 0, 8 },
        };

        private int read_value(denpa_data data)
        {
            return read_value(offsets[(int)data]);
        }

        private void write_value(denpa_data data, int value)
        {
            write_value(offsets[(int)data], value);
        }

        private int read_value(int[] type)
        {
            return read_value(type[0], type[1], type[2]);
        }

        private void write_value(int[] type, int value)
        {
            write_value(type[0], type[1], type[2], value);
        }

        private int read_value(int byteoffset, int bitoffset, int bitcount)
        {
            int value = 0;
            int writemask = 1;
            while (bitcount > 0)
            {
                Byte temp = hexBox1.ByteProvider.ReadByte(byteoffset);
                Byte readmask = 1;
                
                int temp2 = 8;
                while (bitoffset > 0)
                {
                    bitoffset--;
                    temp2--;
                    readmask <<= 1;
                }
                while ((temp2 > 0) && (bitcount > 0))
                {
                    if((temp & readmask) == readmask)
                        value |= writemask;
                    writemask <<= 1;
                    readmask <<= 1;
                    bitcount--;
                    temp2--;
                }

                byteoffset++;
                if (!dontDecryptToolStripMenuItem.Checked)
                    byteoffset++;
                
            }
            return value;
        }

        private void write_value(int byteoffset, int bitoffset, int bitcount, int value)
        {
            int readmask = 1;
            while (bitcount > 0)
            {
                Byte temp = hexBox1.ByteProvider.ReadByte(byteoffset);
                Byte writemask = 1;
                int temp2 = 8;
                byte maskright = 0;
                byte maskleft = 0;
                while (bitoffset > 0)
                {
                    bitoffset--;
                    temp2--;
                    writemask <<= 1;
                    maskright <<= 1;
                    maskright |= 1;
                }
                int bits_to_mask = (bitcount < temp2) ? (temp2 - bitcount) : 0;
                while (bits_to_mask > 0)
                {
                    maskleft >>= 1;
                    maskleft |= 0x80;
                    bits_to_mask--;
                }
                maskright |= maskleft;
                temp &= maskright;
                while ((bitcount > 0) && (temp2 > 0))
                {
                    if ((value & readmask) == readmask)
                        temp |= writemask;
                    writemask <<= 1;
                    readmask <<= 1;
                    bitcount--;
                    temp2--;
                }
                hexBox1.ByteProvider.WriteByte(byteoffset, temp);
                byteoffset++;
                if (!dontDecryptToolStripMenuItem.Checked)
                    byteoffset++;
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int nud1 = (int)numericUpDown1.Value;
            int nud2 = (int)numericUpDown2.Value;
            int nud3 = (int)numericUpDown3.Value;
            if (!dontDecryptToolStripMenuItem.Checked)
            {
                nud1 &= 0xFE;
                if (nud1 == 0x32)
                {
                    numericUpDown3.Maximum = 8 - nud2;
                }
                else if (nud1 == 0x30)
                {
                    numericUpDown3.Maximum = 16 - nud2;
                }
                else
                {
                    numericUpDown3.Maximum = 16;
                }
            }
            else
            {
                if (nud1 == (hexBox1.ByteProvider.Length - 1))
                {
                    numericUpDown3.Maximum = 8 - nud2;
                }
                else if (nud1 == (hexBox1.ByteProvider.Length - 2))
                {
                    numericUpDown3.Maximum = 16 - nud2;
                }
                else
                {
                    numericUpDown3.Maximum = 16;
                }
            }
            numericUpDown1.Value = nud1;
            maskedTextBox1.Text = read_value(nud1, nud2, nud3).ToString("X");
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            int nud5 = (int)numericUpDown5.Value;
            if (nud5 > (offsets.Length - 1))
                nud5 = offsets.Length - 1;
            numericUpDown5.Value = nud5;
            numericUpDown1.Value = offsets[nud5][0];
            numericUpDown2.Value = offsets[nud5][1];
            numericUpDown3.Value = offsets[nud5][2];
        }

        private void numericUpDown4_ValueChanged(object sender, MouseEventArgs e)
        {
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            if (c != '\b' && !((c <= 0x66 && c >= 61) || (c <= 0x46 && c >= 0x41) || (c >= 0x30 && c <= 0x39)))
            {
                e.Handled = true;
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nud1 = (int)numericUpDown1.Value;
            int nud2 = (int)numericUpDown2.Value;
            int nud3 = (int)numericUpDown3.Value;
            int nud4 = Convert.ToInt32(maskedTextBox1.Text, 16);
            write_value(nud1, nud2, nud3, nud4);
        }

        private void getURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Byte[] bytearray = new Byte[0x6A];
            for (int i = 0; i < 0x6A; i++)
                bytearray[i] = hexBox1.ByteProvider.ReadByte(i);
            bytearray = Crypto.Encrypt(bytearray, true);
            String url = "http://www.caitsith2.com/denpa/?data=" + Uri.EscapeUriString(System.Text.Encoding.ASCII.GetString(bytearray));
            try
            {
                Clipboard.SetText(url);
                MessageBox.Show("URL store in clip board", "Denpa QR Code Editor");
            }
            catch
            {
            }
        }

        int[][] stat_data = new int[96][] {
            new int[7] {0,-1,0,0,0,0,0}, new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
            new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],new int[7],
        };
        int stat_count = 1;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String[] stats = textBox1.Text.Split(new char[] {','},6);
            
            if (stats == null) return;
            if (stats.Length != 5) return;
            int index = (int)nudStats.Value;
            if (index < 0) return;

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    stat_data[(stat_count - 1)][i+2] = Convert.ToInt32(stats[i]);
                }
                catch
                {
                    return;
                }
            }
            if (index == 0)
            {
                stat_count = 1;
                for (int i = 1; i < 96; i++)
                {
                    stat_data[i][5] = stat_data[i][6] = stat_data[i][2] = stat_data[i][3] = stat_data[i][4] = 0;
                    stat_data[i][0] = stat_data[i][1] = -1;
                }
            }
            else
            {
                String Temp = "";
                for (int i = 0; i < stat_count; i++)
                {
                    if (stat_data[stat_count - 1][0] != index)
                    {
                        stat_data[stat_count][0] = index;
                        stat_count++;
                    }
                    if (stat_data[stat_count - 1][1] == -1)
                    {
                        stat_data[stat_count - 1][1] = i;
                        for (int j = 0; (j < 5) && (stat_data[stat_count - 1][1] == i); j++)
                        {
                            if (stat_data[index][j+2] != stat_data[i][j+2])
                                stat_data[stat_count - 1][1] = -1;
                        }
                        if (stat_data[stat_count - 1][1] == i)
                        {
                            StatusStripLabel.Text = "Stat Index " + index.ToString() + " is a duplicate of Stat index " + stat_data[i][1].ToString();
                        }
                        else
                        {
                            StatusStripLabel.Text = "Stat Index " + index.ToString() + " is unique";
                        }
                    }
                    Temp += stat_data[i][0].ToString() + ", ";
                    for(int j=0;j<5;j++)
                        Temp += stat_data[i][2+j].ToString() + ", ";
                    Temp += ((stat_data[i][1] == -1) ? "-1" : stat_data[i][1].ToString()) + "\n";
                }
                try
                {
                    Clipboard.SetText(Temp);
                }
                catch
                {
                    MessageBox.Show("Failed to copy stat data to clipboard :(");
                }
            }
            
        }

    }
}
