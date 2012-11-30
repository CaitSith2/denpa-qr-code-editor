using System;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using PushmoLevelEditor.Properties;
using com.google.zxing;
using com.google.zxing.qrcode;
using com.google.zxing.common;

/*
 * Todolist:
 * 
 * TODO: add undo/redo
 * TODO: figure out the rest of the flags of the pushmo...
 * TODO: make drawing even faster by using draw-per-change instead of draw-the-all-thing-on-every-change
 */

namespace PushmoLevelEditor
{
    public partial class FormEditor : Form
    {
        private byte[] QRByteArray;
        private Bitmap qr_code;

        private String[][] strStats = new String[49][];
        private int[][] intStats = new int[49][];

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

        public FormEditor()
        {
            InitializeComponent();
            menuFileNew_Click(null, null);
            update_names();
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
            intStats[(int)AntennaPower.no_antenna] = new int[27] { 
                384, 320, 321, 322, 385, 325, 3, 0, 1, 2, 
                326, 324, 9, 10, 6, 7, 11, 327, 8, 330, 
                328, 329, 333, 387, 386, 331, 332
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
            intStats[(int)AntennaPower.revive_single] = new int[26] { 384, 320, 321, 322, 385, 325, 
                3, 0, 1, 2, 326, 324, 9, 10, 11, 6, 7, 8, 330, 328, 
                329, 333, 387, 386, 331, 332 
            };

            strStats[(int)AntennaPower.attack_water_single] = new String[54] { 
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
            intStats[(int)AntennaPower.attack_water_single] = new int[27] { 
                384, 320, 321, 322, 385, 325, 3, 0, 
                1, 2, 326, 324, 9, 10, 6, 7, 
                11, 327, 8, 330, 328, 329, 333, 387, 
                386, 331, 332
            };
            #endregion

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
            cboColor.SelectedIndex = -1;
            
            cboHeadShape.SelectedIndex = -1;
            cboFaceShapeHairStyle.SelectedIndex = -1;
            cboHairColor.SelectedIndex = -1;
            cboEyes.SelectedIndex = -1;
            cboEyeBrows.SelectedIndex = -1;
            cboNose.SelectedIndex = -1;
            cboFaceColor.SelectedIndex = -1;
            cboMouth.SelectedIndex = -1;
            cboCheeks.SelectedIndex = -1;
            cboGlasses.SelectedIndex = -1;
            cboAntennaPower.SelectedIndex = -1;
            nudStats.Value = -1;
            txtName.Text = "New Denpa";    //Cannot leave the name field in the QR data totally blank or else the game locks up instantly.

            switch (CultureInfo.CurrentCulture.Name)
            {
                case "":
                case "en":
                case "fr":
                case "es":
                    cboRegion.SelectedIndex = -1;
                    break;

                case "ja":
                case "ja-JP":
                    cboRegion.SelectedIndex = 1;
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
                    break;

                default:
                    cboRegion.SelectedIndex = 2;
                    break;
            }

            if ((CultureInfo.CurrentCulture.Name == "ja") || (CultureInfo.CurrentCulture.Name == "ja-JP"))
                cboRegion.SelectedIndex = 1;
            else
                cboRegion.SelectedIndex = 0;

            btnChangeID_Click(sender, e);
            StatusStripLabel.Text = "";
            
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Menu -> QR Code

        private byte[] decrypt_byte_array(byte[] bytearray)
        {
            //int i;
            //if (bytearray == null)
            //    return null;
            //for (i = 0; i < (bytearray.Length / 2); i++)
            //{
            //    switch (bytearray[(i * 2) + 1])
            //    {
            //        case 0x00:
            //        case 0x10:
            //        case 0x20:
            //        case 0x30:
            //        case 0x40:
            //        case 0x50:
            //        case 0x60:
            //        case 0x70:
            //        case 0x80:
            //        case 0x90:
            //        case 0xA0:
            //        case 0xB0:
            //        case 0xC0:
            //        case 0xD0:
            //        case 0xE0:
            //        case 0xF0:
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x01:
            //        case 0x81:
            //            bytearray[(i * 2)] = (byte)((bytearray[(i * 2)] ^ 1) - (((bytearray[(i * 2)] & 1) == 1) ? 2 : 0));
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x02:
            //        case 0x82:
            //            bytearray[(i * 2) + 0] -= 4;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x03:
            //        case 0x83:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 4)== 3)?15:7);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x04:
            //        case 0x84:
            //            bytearray[(i * 2) + 0] -= 16;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x05:
            //        case 0x45:
            //        case 0x85: 
            //        case 0xC5:
            //            var sub_table = new byte[16] { 0x17, 0x13, 0x18, 0x1B, 0x17, 0x23, 0x17, 0x1B, 0x17, 0x13, 0x17, 0x1B, 0x17, 0x23, 0x17, 0x1B };
            //            bytearray[(i * 2) + 0] -= sub_table[bytearray[(i * 2) + 0] & 0x0F];
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x25:
            //        case 0x65:
            //        case 0xA5:
            //        case 0xE5:
            //            break;
            //        case 0x06:
            //        case 0x86:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 8) < 4) ? 0x1C : 0x2C);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x07:
            //        case 0x47:
            //        case 0x87:
            //        case 0xC7:
            //            bytearray[(i * 2) + 0] -= (byte)(((bytearray[(i * 2) + 0] % 8) == 7) ? 0x3F : 0x2F);
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x08:
            //        case 0x18:
            //        case 0x28:
            //        case 0x38:
            //        case 0x88:
            //        case 0x98:
            //        case 0xA8:
            //        case 0xB8:
            //            bytearray[(i * 2) + 0] -= 64;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x09:
            //        case 0x29:
            //        case 0x89:
            //        case 0xA9:
            //            var sub_table_09 = new byte[16] { 0x4F, 0x43, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x63, 0x4F, 0x53, 0x4F, 0x53, 0x4F, 0x53 };
            //            bytearray[(i * 2) + 0] -= sub_table_09[bytearray[(i * 2) + 0] & 0x0F];
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x0F:
            //        case 0x2F:
            //        case 0x4F:
            //        case 0x6F:
            //        case 0x8F:
            //        case 0xAF:
            //        case 0xCF:
            //        case 0xEF:
            //            bytearray[(i * 2)] += 0x21;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x11:
            //        case 0x71:
            //        case 0x91:
            //        case 0xF1:
            //            var offset = bytearray[(i * 2)];
            //            bytearray[(i * 2)] = (byte)((bytearray[(i * 2)] ^ 1) - (((bytearray[(i * 2)] & 1) == 1) ? 2 : 0));
            //            if((offset & 0x1F) != 0x01)
            //                bytearray[(i * 2)] -= 0x20;
            //            if ((offset & 0x1F) == 0x11)
            //                bytearray[(i * 2)] -= 0x30;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x3F:
            //        case 0xBF:
            //            bytearray[(i * 2)] += 0x81;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;

            //        case 0x1F:
            //        case 0x5F:
            //        case 0x9F:
            //        case 0xDF:
            //            bytearray[(i * 2)] += 0x41;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
                    
                    
                    
            //        case 0x77:
            //        case 0xF7:
            //            if ((bytearray[(i * 2)] % 8) == 7)
            //                bytearray[(i * 2)] += 0x61;
            //            else
            //                bytearray[(i * 2)] += 0x51;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x78:
            //        case 0xF8:
            //            bytearray[(i * 2) + 0] += 0x40;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x79:
            //        case 0xF9:
            //            if ((bytearray[(i * 2)] % 8) == 1)
            //                bytearray[(i * 2)] += 0x3D;
            //            else if ((bytearray[(i * 2)] % 2) == 0)
            //                bytearray[(i * 2)] += 0x31;
            //            else
            //                bytearray[(i * 2)] += 0x2D;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7A:
            //        case 0xFA:
            //            if ((bytearray[(i * 2)] % 8) < 4)
            //                bytearray[(i * 2)] += 0x2C;
            //            else
            //                bytearray[(i * 2)] += 0x1C;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7B:
            //        case 0xFB:
            //            if ((bytearray[(i * 2)] % 8) == 3)
            //                bytearray[(i * 2)] += 0x21;
            //            else if ((bytearray[(i * 2)] % 8) == 7)
            //                bytearray[(i * 2)] += 0x11;
            //            else
            //                bytearray[(i * 2)] += 0x19;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7C:
            //        case 0xFC:
            //            bytearray[(i * 2) + 0] += 16;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7D:
            //        case 0xFD:
            //            if ((bytearray[(i * 2)] % 4) == 1)
            //                bytearray[(i * 2)] += 0x0D;
            //            else if ((bytearray[(i * 2)] % 4) == 3)
            //                bytearray[(i * 2)] += 0x05;
            //            else
            //                bytearray[(i * 2)] += 0x09;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x3E:
            //        case 0x7E:
            //        case 0xBE:
            //        case 0xFE:
            //            bytearray[(i * 2) + 0] += 4;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        case 0x7F:
            //        case 0xFF:
            //            bytearray[(i * 2) + 0] += 1;
            //            bytearray[(i * 2) + 1] = 0;
            //            break;
            //        default:
            //            break;
            //    }
               
            //}

            return bytearray;
        }

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

            data = QRByteArray;
            if (data == null)
                return null;
            var str = Encoding.GetEncoding(encoding).GetString(data);
            var hints = new Hashtable { { EncodeHintType.CHARACTER_SET, encoding } };
            return writer.encode(str, BarcodeFormat.QR_CODE, size, size, hints); 
        }

        #endregion


        #region Check for updates
        private void bwCheckForUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private bool IsNewerAvailable(string newerVersion)
        {
            var thisVersion = Version.Parse(Application.ProductVersion);
            var remoteVersion = Version.Parse(newerVersion);
            return remoteVersion.CompareTo(thisVersion) > 0;
        }

        private void bwCheckForUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
            var matrix = GetQRMatrix(100);
            if (matrix == null)
                return;
            var img = new Bitmap(100,100);
            var g = Graphics.FromImage(img);
            g.Clear(Color.White);
            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                    if (matrix.get_Renamed(x, y) != -1)
                        g.FillRectangle(Brushes.Black, x * 1, y * 1, 1, 1);
            if (btnSwitchPicBox.Text == "Feature")
                picBox.Image = img;
            else
                picBox.Image = picBox2.Image;
            qr_code = img;
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
                QRByteArray = decrypt_byte_array(byteArray);
                if (hexBox1.ByteProvider != null)
                {
                    IDisposable byteProvider = hexBox1.ByteProvider as IDisposable;
                    if (byteProvider != null)
                        byteProvider.Dispose();
                    hexBox1.ByteProvider = null;
                }
                hexBox1.ByteProvider = new Be.Windows.Forms.DynamicByteProvider(QRByteArray);
                hexBox1_KeyPress(sender, null);

                var temp = new byte[0];
                temp = new Byte[QRByteArray.Length];
                QRByteArray.CopyTo(temp, 0);    //Make sure any tampering with the combo boxes do NOT mess with this data.

                

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

        private void cboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte temp;
            int color_index = cboColor.SelectedIndex;
            bool solid_color_enable;
            int antenna_index = cboAntennaPower.SelectedIndex;

            if (color_index == -1) return;
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
                //We now modify this index, based upon Antenna power selected.
                temp = hexBox1.ByteProvider.ReadByte(0x12);
                temp &= 0x07;
                temp |= (byte)((color_index - ((solid_color_enable)?1:0)) << 3);
                hexBox1.ByteProvider.WriteByte(0x12,temp);
                hexBox1.ByteProvider.WriteByte(0x13,0);
                if(solid_color_enable)
                    hexBox1.ByteProvider.WriteByte(0x24, 0x5C);
                else
                    hexBox1.ByteProvider.WriteByte(0x24, 0x00);
                hexBox1.ByteProvider.WriteByte(0x25, 0);
            }
            else
            {
                temp = hexBox1.ByteProvider.ReadByte(0x12);
                temp &= 0x07;
                temp |= (byte)((cboColor.SelectedIndex - 7) << 3);
                hexBox1.ByteProvider.WriteByte(0x12, temp);
                hexBox1.ByteProvider.WriteByte(0x13, 0);
                hexBox1.ByteProvider.WriteByte(0x24, 0xDF);
                hexBox1.ByteProvider.WriteByte(0x25, 0);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboAntennaPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboColor_SelectedIndexChanged(sender, e);  //Colors depend on Antenna power
            cboFaceShapeHairStyle_SelectedIndexChanged(sender, e);  //So does the Hair style.
            StatusStripLabel.Text = ""; 
            int index = cboAntennaPower.SelectedIndex;
            if (index == -1) return;

            String[] AP_head_shape = new String[25] {
                "N/A",
                "7","7","7","7","7","7","7",
                "12","12","12","12",
                "15","15","15","15","15","15","15",
                "20","20","20","24","20","24"
            };

            if (strStats[index] != null)
            {
                cboStats.Items.Clear();
                if (index == 0)
                    cboStats.Items.AddRange(strStats[index]);
                else
                {
                    for (int i = 0; i < intStats[index].Length; i++)
                        cboStats.Items.Add(strStats[index][(i * 2)] + AP_head_shape[cboHeadShape.SelectedIndex + 1] + strStats[index][(i * 2) + 1]); 
                }
                for (int i = 0; i < intStats[index].Length; i++)
                {
                    if ((int)nudStats.Value == intStats[index][i])
                        cboStats.SelectedIndex = i;
                }
            }
            else
            {
                cboStats.Items.Clear();
            }


            int temp = hexBox1.ByteProvider.ReadByte(0x10) & 0xC0;
            if (index == 0)
            {
                hexBox1.ByteProvider.WriteByte(0x28, 0);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else if ((index >= 1) && (index <= 12))
            {
                temp |= ((index - 1) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD6);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else if ((index >= 13) && (index <= 24))
            {
                temp |= ((index - 13) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD7);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            else
            {
                temp |= ((index - 25) & 0x3F);
                hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x11, 0);
                hexBox1.ByteProvider.WriteByte(0x28, 0xD8);
                hexBox1.ByteProvider.WriteByte(0x29, 0);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.TextLength > 0)
            {
                byte[] unicode_str = System.Text.Encoding.Unicode.GetBytes(txtName.Text);
                
                for (int i = 0; i < txtName.TextLength; i++)
                {
                    hexBox1.ByteProvider.WriteByte(0x34 + (i*4), unicode_str[(i*2)]);
                    hexBox1.ByteProvider.WriteByte(0x35 + (i * 4), 0);
                    hexBox1.ByteProvider.WriteByte(0x36 + (i * 4), unicode_str[(i*2)+1]);
                    hexBox1.ByteProvider.WriteByte(0x37 + (i * 4), 0);
                }
                hexBox1.ByteProvider.WriteByte(0x34 + (txtName.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x35 + (txtName.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x36 + (txtName.TextLength * 4), 0);
                hexBox1.ByteProvider.WriteByte(0x37 + (txtName.TextLength * 4), 0);
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

            String[] AP_head_shape = new String[25] {
                "N/A",
                "7","7","7","7","7","7","7",
                "12","12","12","12",
                "15","15","15","15","15","15","15",
                "20","20","20","24","20","24"
            };

            if (AntennaIndex != -1)
            {
                if (strStats[AntennaIndex] != null)
                {
                    cboStats.Items.Clear();
                    for (int i = 0; i < intStats[AntennaIndex].Length; i++)
                    {
                        if (AntennaIndex != 0)
                            cboStats.Items.Add(strStats[AntennaIndex][(i * 2)] + AP_head_shape[cboHeadShape.SelectedIndex + 1] + strStats[AntennaIndex][(i * 2) + 1]);
                        else
                            cboStats.Items.Add(strStats[AntennaIndex][i]);
                        if ((int)nudStats.Value == intStats[AntennaIndex][i])
                            cboStats.SelectedIndex = i;
                    }
                }
                else
                {
                    cboStats.Items.Clear();
                }
            }
            else
            {
                cboStats.Items.Clear();
            }


            int temp;
            temp = hexBox1.ByteProvider.ReadByte(0x14);
            temp &= 0xE0;
            if ((index >= 0) && (index <= 10))
            {
                temp |= (index - 0);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            else if ((index >= 11) && (index <= 17))
            {
                temp |= (index - 11);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0x28);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            else
            {
                temp |= (index - 18);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x26, 0x30);
                hexBox1.ByteProvider.WriteByte(0x27, 0);
            }
            hexBox1.Refresh();
            picBox2.Image = head[index];
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
            hexBox1.ByteProvider.WriteByte(0x0D, 0x00);
            hexBox1.ByteProvider.WriteByte(0x0F, 0x00);
            Random random = new Random();
            var rand_data = new Byte[1];
            random.NextBytes(rand_data);
            hexBox1.ByteProvider.WriteByte(0x0C, rand_data[0]);
            random.NextBytes(rand_data);
            hexBox1.ByteProvider.WriteByte(0x0E, rand_data[0]);
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboFaceShapeHairStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFaceShapeHairStyle.SelectedIndex;
            int antenna = cboAntennaPower.SelectedIndex;
            
            if (index == -1) return;
            if (antenna == -1) { StatusStripLabel.Text = "Antenna Power required"; return; }
            int temp = hexBox1.ByteProvider.ReadByte(0x14) & 0x1F;
            int temp2 = hexBox1.ByteProvider.ReadByte(0x16) & 0xF8;
            if (index < 9)
            {
                temp |= ((index & 0x7) << 5);
                temp2 |= ((index & 0x18) >> 3);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp2);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
                hexBox1.ByteProvider.WriteByte(0x2A, 0x00);
                hexBox1.ByteProvider.WriteByte(0x2B, 0);
            }
            else
            {
                temp |= (((index - 9) & 0x7) << 5);
                temp2 |= (((index - 9) & 0x18) >> 3);
                hexBox1.ByteProvider.WriteByte(0x14, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x15, 0);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp2);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
                if (antenna == 0)
                    hexBox1.ByteProvider.WriteByte(0x2A, 0x0C);
                else
                    hexBox1.ByteProvider.WriteByte(0x2A, 0x0B);
                hexBox1.ByteProvider.WriteByte(0x2B, 0);
            }
            hexBox1.Refresh();
            picBox2.Image = faceshape[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboHairColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboHairColor.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x18) & 0xE0;
            temp |= (index & 0x1F);
            hexBox1.ByteProvider.WriteByte(0x18, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x19, 0);
            hexBox1.Refresh();
            picBox2.Image = haircolor[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboEyes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboEyes.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x18) & 0x1F;
            temp |= ((index & 0x07) << 5);
            hexBox1.ByteProvider.WriteByte(0x18, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x19, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x1A) & 0xFC;
            temp |= ((index & 0x18) >> 3);
            hexBox1.ByteProvider.WriteByte(0x1A, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1B, 0);
            hexBox1.Refresh();
            picBox2.Image = eyes[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboEyeBrows_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboEyeBrows.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0x3F;
            temp |= ((index & 0x03) << 6);
            hexBox1.ByteProvider.WriteByte(0x1C, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1D, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x1E) & 0xFE;
            temp |= ((index & 0x04) >> 2);
            hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1F, 0);
            hexBox1.Refresh();
            picBox2.Image = eye_brow[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboNose_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboNose.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0x87;
            temp |= ((index & 0x0F) << 3);
            hexBox1.ByteProvider.WriteByte(0x1A, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1B, 0);
            hexBox1.Refresh();
            picBox2.Image = nose[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboFaceColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboFaceColor.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x16) & 0xE7;
            if (index < 2)
            {
                hexBox1.ByteProvider.WriteByte(0x30, 0x0C);
                hexBox1.ByteProvider.WriteByte(0x31, 0);
                temp |= ((index & 0x03) << 3);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x30, 0x00);
                hexBox1.ByteProvider.WriteByte(0x31, 0);
                temp |= (((index - 2) & 0x03) << 3);
                hexBox1.ByteProvider.WriteByte(0x16, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x17, 0);
            }
            hexBox1.Refresh();
            picBox2.Image = face_color[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboMouth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboMouth.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x1C) & 0xC0;
            temp |= (index & 0x1F);
            hexBox1.ByteProvider.WriteByte(0x1C, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x1D, 0);
            hexBox1.Refresh();
            picBox2.Image = mouth[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboCheeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboCheeks.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x1E) & 0x07;
            if (index < 1)
            {
                hexBox1.ByteProvider.WriteByte(0x2C, 0);
                hexBox1.ByteProvider.WriteByte(0x2D, 0);
                temp |= ((index & 0x07) << 3);
                hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x1F, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x2C, 0x5A);
                hexBox1.ByteProvider.WriteByte(0x2D, 0);
                temp |= (((index - 1) & 0x07) << 3);
                hexBox1.ByteProvider.WriteByte(0x1E, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x1F, 0);
            }
            hexBox1.Refresh();
            picBox2.Image = cheek[index];
            hexBox1_KeyPress(null, null);
        }

        private void cboGlasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboGlasses.SelectedIndex;
            if (index == -1) return;
            int temp = hexBox1.ByteProvider.ReadByte(0x20) & 0xE0;
            if (index < 1)
            {
                hexBox1.ByteProvider.WriteByte(0x2E, 0);
                hexBox1.ByteProvider.WriteByte(0x2F, 0);
                temp |= ((index & 0x1F));
                hexBox1.ByteProvider.WriteByte(0x20, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x21, 0);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x2E, 0x30);
                hexBox1.ByteProvider.WriteByte(0x2F, 0);
                temp |= (((index - 1) & 0x1F));
                hexBox1.ByteProvider.WriteByte(0x20, (byte)temp);
                hexBox1.ByteProvider.WriteByte(0x21, 0);
            }
            hexBox1.Refresh();
            picBox2.Image = glasses[index];
            hexBox1_KeyPress(null, null);
        }

        private void nudStats_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)nudStats.Value;
            int antenna_index = cboAntennaPower.SelectedIndex;
            if (index < 0) return;

            if (antenna_index != -1)
            {
                if (intStats[antenna_index] != null)
                {
                    cboStats.SelectedIndex = -1;
                    for (int i = 0; i < intStats[antenna_index].Length; i++)
                    {
                        if ((int)nudStats.Value == intStats[antenna_index][i])
                        {
                            cboStats.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }

            int temp = hexBox1.ByteProvider.ReadByte(0x10) & 0x3F;
            temp |= ((index & 0x03) << 6);
            hexBox1.ByteProvider.WriteByte(0x10, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x11, 0);
            temp = hexBox1.ByteProvider.ReadByte(0x12) & 0xF8;
            temp |= ((index & 0x1C) >> 2);
            hexBox1.ByteProvider.WriteByte(0x12, (byte)temp);
            hexBox1.ByteProvider.WriteByte(0x13, 0);
            hexBox1.ByteProvider.WriteByte(0x22, (byte)((index & 0x1E0) >> 5));
            hexBox1.ByteProvider.WriteByte(0x23, 0);
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
            if (cboRegion.SelectedIndex == 1)
            {
                hexBox1.ByteProvider.WriteByte(0x00, 0x62);
                hexBox1.ByteProvider.WriteByte(0x01, 0x00);
                hexBox1.ByteProvider.WriteByte(0x02, 0x58);
                hexBox1.ByteProvider.WriteByte(0x03, 0x00);
                hexBox1.ByteProvider.WriteByte(0x04, 0x38);
                hexBox1.ByteProvider.WriteByte(0x05, 0x00);
                hexBox1.ByteProvider.WriteByte(0x06, 0x30);
                hexBox1.ByteProvider.WriteByte(0x07, 0x00);
            }
            else if (cboRegion.SelectedIndex == 0)
            {
                hexBox1.ByteProvider.WriteByte(0x00, 0x41);
                hexBox1.ByteProvider.WriteByte(0x01, 0x00);
                hexBox1.ByteProvider.WriteByte(0x02, 0x68);
                hexBox1.ByteProvider.WriteByte(0x03, 0x00);
                hexBox1.ByteProvider.WriteByte(0x04, 0x34);
                hexBox1.ByteProvider.WriteByte(0x05, 0x00);
                hexBox1.ByteProvider.WriteByte(0x06, 0x33);
                hexBox1.ByteProvider.WriteByte(0x07, 0x00);
            }
            else
            {
                hexBox1.ByteProvider.WriteByte(0x00, 0x6A);
                hexBox1.ByteProvider.WriteByte(0x01, 0x00);
                hexBox1.ByteProvider.WriteByte(0x02, 0x33);
                hexBox1.ByteProvider.WriteByte(0x03, 0x00);
                hexBox1.ByteProvider.WriteByte(0x04, 0x5A);
                hexBox1.ByteProvider.WriteByte(0x05, 0x00);
                hexBox1.ByteProvider.WriteByte(0x06, 0x77);
                hexBox1.ByteProvider.WriteByte(0x07, 0x00);
            }
            hexBox1.Refresh();
            hexBox1_KeyPress(null, null);
        }

        private void cboStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stat_index = cboStats.SelectedIndex;
            int antenna_index = cboAntennaPower.SelectedIndex;
            if (stat_index == -1) return;
            if (antenna_index == -1) { StatusStripLabel.Text = "Antenna Power required"; return; }
            if (intStats[antenna_index] == null) return;
            nudStats.Value = intStats[antenna_index][stat_index];
        }
    }
}
