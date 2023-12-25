--
-- PostgreSQL database dump
--

-- Dumped from database version 14.4
-- Dumped by pg_dump version 14.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: department_position; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.department_position (
    id integer NOT NULL,
    department_id integer,
    position_id integer,
    employee_id integer
);


ALTER TABLE public.department_position OWNER TO postgres;

--
-- Name: department_position_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.department_position_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.department_position_id_seq OWNER TO postgres;

--
-- Name: department_position_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.department_position_id_seq OWNED BY public.department_position.id;


--
-- Name: departments; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.departments (
    id integer NOT NULL,
    name text
);


ALTER TABLE public.departments OWNER TO postgres;

--
-- Name: departments_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.departments_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.departments_id_seq OWNER TO postgres;

--
-- Name: departments_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.departments_id_seq OWNED BY public.departments.id;


--
-- Name: employees; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employees (
    id integer NOT NULL,
    first_name text,
    last_name text,
    patronymic text,
    address text,
    phone text,
    birth_date date,
    employment_date date,
    salary numeric
);


ALTER TABLE public.employees OWNER TO postgres;

--
-- Name: employees_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.employees_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.employees_id_seq OWNER TO postgres;

--
-- Name: employees_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.employees_id_seq OWNED BY public.employees.id;


--
-- Name: positions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.positions (
    id integer NOT NULL,
    name text
);


ALTER TABLE public.positions OWNER TO postgres;

--
-- Name: positions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.positions_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.positions_id_seq OWNER TO postgres;

--
-- Name: positions_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.positions_id_seq OWNED BY public.positions.id;


--
-- Name: department_position id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_position ALTER COLUMN id SET DEFAULT nextval('public.department_position_id_seq'::regclass);


--
-- Name: departments id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments ALTER COLUMN id SET DEFAULT nextval('public.departments_id_seq'::regclass);


--
-- Name: employees id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees ALTER COLUMN id SET DEFAULT nextval('public.employees_id_seq'::regclass);


--
-- Name: positions id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.positions ALTER COLUMN id SET DEFAULT nextval('public.positions_id_seq'::regclass);


--
-- Data for Name: department_position; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.department_position (id, department_id, position_id, employee_id) FROM stdin;
1	5	4	1
2	7	8	2
3	4	7	3
4	1	4	4
5	5	2	5
6	1	7	6
7	7	8	7
8	4	3	8
9	6	8	9
10	7	3	10
11	5	3	11
12	5	7	12
13	1	4	13
14	4	8	14
15	3	6	15
16	1	5	16
17	7	2	17
18	7	2	18
19	5	4	19
20	4	4	20
21	7	8	21
22	2	7	22
23	1	5	23
24	7	3	24
25	6	8	25
26	2	5	26
27	3	3	27
28	3	4	28
29	3	6	29
30	7	6	30
31	1	7	31
32	5	4	32
33	3	8	33
34	2	6	34
35	2	5	35
36	1	6	36
37	4	6	37
38	3	3	38
39	3	6	39
40	5	6	40
41	1	3	41
42	4	5	42
43	6	5	43
44	4	1	44
45	3	3	45
46	7	5	46
47	4	1	47
48	5	1	48
49	3	3	49
50	6	4	50
51	6	1	51
52	3	6	52
53	6	4	53
54	4	3	54
55	7	6	55
56	6	5	56
57	7	1	57
58	7	2	58
59	1	8	59
60	2	6	60
61	1	1	61
62	1	4	62
63	1	2	63
64	1	6	64
65	6	5	65
66	1	8	66
67	3	7	67
68	7	2	68
69	6	8	69
70	2	2	70
71	2	3	71
72	7	6	72
73	6	2	73
74	4	2	74
75	4	8	75
76	5	4	76
77	1	3	77
78	3	6	78
79	1	8	79
80	6	1	80
81	5	8	81
82	4	3	82
83	5	6	83
84	2	1	84
85	1	1	85
86	7	7	86
87	6	8	87
88	2	3	88
89	4	3	89
90	1	3	90
91	3	5	91
92	5	2	92
93	4	8	93
94	5	8	94
95	1	4	95
96	7	3	96
97	2	3	97
98	4	5	98
99	2	4	99
100	1	3	100
101	1	6	101
102	2	4	102
103	4	7	103
104	7	6	104
105	2	4	105
106	6	4	106
107	3	3	107
108	3	8	108
109	5	8	109
110	6	7	110
111	3	6	111
112	4	8	112
113	4	8	113
114	4	8	114
115	3	4	115
116	2	2	116
117	7	6	117
118	7	1	118
119	4	2	119
120	4	2	120
121	3	6	121
122	7	8	122
123	7	3	123
124	7	2	124
125	6	4	125
126	7	1	126
127	4	6	127
128	4	7	128
129	2	7	129
130	5	1	130
131	2	6	131
132	1	2	132
133	3	3	133
134	7	6	134
135	3	2	135
136	1	5	136
137	3	6	137
138	1	1	138
139	6	8	139
140	1	8	140
141	6	7	141
142	2	8	142
143	7	4	143
144	2	4	144
145	2	7	145
146	6	6	146
147	4	7	147
148	7	8	148
149	6	6	149
150	5	3	150
151	3	7	151
152	3	6	152
153	7	7	153
154	7	2	154
155	7	4	155
156	6	3	156
157	6	8	157
158	4	5	158
159	3	5	159
160	5	2	160
161	6	3	161
162	2	3	162
163	2	7	163
164	3	8	164
165	7	7	165
166	3	8	166
167	1	6	167
168	6	3	168
169	3	8	169
170	1	7	170
171	5	6	171
172	3	8	172
173	3	4	173
174	4	5	174
175	5	3	175
176	1	3	176
177	3	6	177
178	2	8	178
179	6	7	179
180	1	3	180
181	6	7	181
182	3	7	182
183	7	5	183
184	3	8	184
185	2	3	185
186	5	8	186
187	4	2	187
188	2	3	188
189	5	8	189
190	3	1	190
191	2	8	191
192	7	2	192
193	7	2	193
194	3	6	194
195	5	3	195
196	2	5	196
197	1	7	197
198	6	5	198
199	1	3	199
200	2	5	200
\.


--
-- Data for Name: departments; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.departments (id, name) FROM stdin;
1	Sales
2	HR
3	Accounting
4	Infrastructures
5	Marketing
6	Product Development
7	Administration Department
\.


--
-- Data for Name: employees; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employees (id, first_name, last_name, patronymic, address, phone, birth_date, employment_date, salary) FROM stdin;
1	Alisia	Longstreth	Piggy	AlisiaLongstreth0@gmail.com	044-521-19-76	1987-11-02	1992-10-19	68509.73
2	Arianna	Magdaleno	Boyce	AriannaMagdaleno0@gmail.com	044-412-70-89	1993-10-08	1993-04-09	7365.29
3	Dave	Irland	Far	DaveIrland0@gmail.com	044-396-10-68	1998-05-15	1981-09-11	63.54
4	Cristina	Jaquet	Benoit	CristinaJaquet0@gmail.com	044-893-85-95	1987-09-19	1998-02-13	27537.79
5	Shavonda	Capinpin	Brant	ShavondaCapinpin0@gmail.com	044-249-79-53	1984-02-22	1988-04-03	7170.79
6	Lavelle	Sarne	Quent	LavelleSarne0@gmail.com	044-824-94-82	1991-06-15	1989-01-09	45124.48
7	Vina	Balfany	Cullie	VinaBalfany0@gmail.com	044-410-31-42	1980-10-13	1986-02-03	22884.85
8	Sherril	Netland	Kenny	SherrilNetland0@gmail.com	044-123-72-78	1987-11-12	1983-01-24	5484.85
9	Jaime	Zingaro	Putnam	JaimeZingaro0@gmail.com	044-362-51-85	1984-09-22	1988-09-23	43084.09
10	Skye	Lites	Homere	SkyeLites0@gmail.com	044-738-68-83	1996-05-16	1985-08-01	31548.46
11	Mario	Engblom	Egbert	MarioEngblom0@gmail.com	044-974-98-35	1994-11-17	1984-03-02	58146.6
12	Tami	Gomey	Monti	TamiGomey0@gmail.com	044-680-13-25	1996-07-14	1984-06-06	17718.63
13	Leonor	Jindra	Elwin	LeonorJindra0@gmail.com	044-273-92-94	1988-11-09	1991-11-08	36197.33
14	Shani	Syvertsen	Anthony	ShaniSyvertsen0@gmail.com	044-232-85-74	1984-01-22	1993-02-23	22928.37
15	Dawna	Boglioli	Morten	DawnaBoglioli0@gmail.com	044-764-14-85	1980-01-18	1984-05-18	11475.72
16	Royal	Adamsen	Griffin	RoyalAdamsen0@gmail.com	044-160-81-36	1996-05-11	1983-11-20	51006.92
17	Raeann	Courser	Morley	RaeannCourser0@gmail.com	044-137-24-14	1996-07-16	1990-04-06	44825.54
18	Laurence	Gergel	Nial	LaurenceGergel0@gmail.com	044-261-11-86	1993-11-11	1996-04-10	56987.03
19	Veta	Chrzan	Filmore	VetaChrzan0@gmail.com	044-114-19-66	1990-01-01	1993-09-03	33950.5
20	Lonna	Debrot	Beaufort	LonnaDebrot0@gmail.com	044-352-36-57	1999-08-01	1986-05-02	86358.88
21	Charlie	Platas	Humfried	CharliePlatas0@gmail.com	044-553-77-88	1996-06-23	1993-08-08	14705.98
22	Lesley	Sofer	Kelley	LesleySofer0@gmail.com	044-272-15-39	1980-10-08	1986-02-22	2892.24
23	Lupita	Foshee	Jeddy	LupitaFoshee0@gmail.com	044-507-10-48	1990-03-07	1998-10-06	4247.67
24	Janina	Slaight	Torey	JaninaSlaight0@gmail.com	044-983-21-83	1999-05-22	1997-01-11	72161.06
25	Tanna	Haislett	Forester	TannaHaislett0@gmail.com	044-290-61-29	1995-01-04	1984-01-02	8243.97
26	Lezlie	Kotonski	Filippo	LezlieKotonski0@gmail.com	044-380-68-13	1990-03-22	1981-09-22	15452.87
27	Soraya	Richmon	Chad	SorayaRichmon0@gmail.com	044-970-27-20	1998-07-10	1991-04-24	2313.41
28	Joann	Cecere	Wainwright	JoannCecere0@gmail.com	044-306-14-44	1985-11-06	1980-09-16	19088.89
29	Tanesha	Rakes	Osbert	TaneshaRakes0@gmail.com	044-386-57-64	1992-01-06	1994-02-10	19936.95
30	Joyce	Epson	Paxton	JoyceEpson0@gmail.com	044-188-28-29	1987-03-10	1998-09-17	8096.19
31	Tequila	Yant	Cy	TequilaYant0@gmail.com	044-900-23-67	1987-05-09	1995-02-10	93789.91
32	Len	Gongalves	Lou	LenGongalves0@gmail.com	044-658-86-25	1986-01-08	1998-05-18	26165.98
33	Misty	Flagg	Ariel	MistyFlagg0@gmail.com	044-432-69-39	1993-01-17	1980-11-21	2665.5
34	Lorina	Abdella	Bondon	LorinaAbdella0@gmail.com	044-656-15-15	1993-06-24	1985-07-16	64138.15
35	Taunya	Gnas	Wyndham	TaunyaGnas0@gmail.com	044-579-26-48	1995-05-03	1992-08-24	17063.92
36	Rebecca	Waitkus	Derick	RebeccaWaitkus0@gmail.com	044-252-74-48	1994-08-07	1999-06-22	51440.12
37	Veta	Cusick	Goddard	VetaCusick0@gmail.com	044-102-44-37	1980-02-19	1987-10-11	66778.66
38	Ernie	Konecni	Tirrell	ErnieKonecni0@gmail.com	044-345-75-20	1982-01-20	1993-01-01	3044.44
39	Vern	Hassell	Ryan	VernHassell0@gmail.com	044-259-11-60	1987-04-13	1983-04-03	28514.94
40	Bella	Lavinder	Bradley	BellaLavinder0@gmail.com	044-587-64-43	1982-04-21	1995-02-16	11703.57
41	Jodie	Czekanski	Demetre	JodieCzekanski0@gmail.com	044-482-15-20	1991-02-08	1984-05-07	72841.16
42	Archie	Rehnert	Mathias	ArchieRehnert0@gmail.com	044-908-94-56	1989-09-02	1998-03-19	31886.87
43	Adrienne	Lacey	Boote	AdrienneLacey0@gmail.com	044-655-41-12	1990-07-05	1990-09-09	30249.73
44	Latonia	Falsetta	Johann	LatoniaFalsetta0@gmail.com	044-599-17-71	1985-04-16	1998-06-13	28912.51
45	Marcella	Fuchs	Rafael	MarcellaFuchs0@gmail.com	044-110-88-77	1981-04-05	1990-02-12	1276.41
46	Jamel	Baudler	Maxy	JamelBaudler0@gmail.com	044-632-72-60	1995-04-17	1993-06-15	28586.44
47	Luana	Kristy	Dario	LuanaKristy0@gmail.com	044-384-74-33	1986-03-08	1999-01-18	55011.47
48	Holly	Fitzgerald	Mannie	HollyFitzgerald0@gmail.com	044-961-42-28	1990-09-03	1988-08-14	15759.66
49	Zenia	Rosenburg	Normie	ZeniaRosenburg0@gmail.com	044-151-77-81	1988-10-16	1999-08-14	21877.58
50	Patria	Smar	Sheff	PatriaSmar0@gmail.com	044-515-28-80	1993-05-23	1994-06-03	18035.83
51	Yvette	Weigart	Crosby	YvetteWeigart0@gmail.com	044-908-59-73	1997-11-21	1982-05-24	30770.65
52	Fallon	Asken	Edward	FallonAsken0@gmail.com	044-268-25-88	1984-06-11	1991-06-16	4294.37
53	Blondell	Dohman	Windham	BlondellDohman0@gmail.com	044-339-26-35	1985-09-13	1997-10-09	7591.07
54	Leana	Mill	Elijah	LeanaMill0@gmail.com	044-676-56-96	1980-05-19	1995-05-21	28075.91
55	Britta	Munderville	Errick	BrittaMunderville0@gmail.com	044-372-62-70	1987-11-10	1988-02-19	12769.42
56	Deedra	Maugeri	Keith	DeedraMaugeri0@gmail.com	044-762-59-16	1997-03-20	1986-08-03	4890.33
57	Corinne	Tolbent	Bud	CorinneTolbent0@gmail.com	044-895-69-95	1980-09-16	1994-11-23	55635.09
58	Teodora	Nybo	Elsworth	TeodoraNybo0@gmail.com	044-764-56-88	1985-04-08	1990-06-13	45371
59	Josephina	Crudup	Kenn	JosephinaCrudup0@gmail.com	044-464-77-85	1982-10-13	1982-06-15	29759.54
60	Carolyne	Torre	Alair	CarolyneTorre0@gmail.com	044-339-47-32	1999-08-07	1998-08-14	40840.7
61	Brandie	Madine	Hiram	BrandieMadine0@gmail.com	044-567-73-14	1992-03-12	1996-02-18	14314.44
62	Donnette	Liehr	Earle	DonnetteLiehr0@gmail.com	044-938-35-20	1997-01-13	1984-01-15	24022.33
63	Nilda	Kitchen	Cary	NildaKitchen0@gmail.com	044-789-84-97	1999-08-07	1995-02-14	20830.69
64	Li	Mayher	Huntley	LiMayher0@gmail.com	044-496-98-89	1983-09-18	1993-07-06	10343.89
65	Ines	Fernstaedt	Milo	InesFernstaedt0@gmail.com	044-862-75-54	1992-01-04	1997-11-12	49273.29
66	Faviola	Kaduk	Emmett	FaviolaKaduk0@gmail.com	044-720-97-34	1994-05-12	1994-05-20	23537.36
67	Magan	Butenhoff	Ty	MaganButenhoff0@gmail.com	044-222-42-30	1995-07-14	1981-11-21	14271.63
68	Towanda	Atwater	Zane	TowandaAtwater0@gmail.com	044-540-16-43	1988-04-18	1986-08-03	73354.01
69	Jamee	Kuczynski	Piggy	JameeKuczynski0@gmail.com	044-806-67-49	1989-08-21	1992-11-23	84214.24
70	Clement	Reeks	Matty	ClementReeks0@gmail.com	044-416-29-24	1989-06-14	1987-06-19	4765.37
71	Elvia	Jubinville	Jory	ElviaJubinville0@gmail.com	044-439-84-83	1986-11-11	1995-06-23	12058.01
72	Joey	Milledge	Jeremiah	JoeyMilledge0@gmail.com	044-695-84-25	1981-04-15	1981-01-06	13444.11
73	Patti	Magri	Woody	PattiMagri0@gmail.com	044-511-25-32	1983-07-21	1987-04-13	21087.88
74	Lawana	Timberlake	Spike	LawanaTimberlake0@gmail.com	044-688-82-55	1991-06-03	1999-05-12	588.59
75	Madalyn	Casales	Bond	MadalynCasales0@gmail.com	044-581-60-19	1999-08-04	1991-03-23	9859.71
76	Page	Lubow	Rees	PageLubow0@gmail.com	044-474-93-56	1999-09-19	1990-01-23	47170.02
77	Un	Prewett	Bron	UnPrewett0@gmail.com	044-600-36-70	1985-04-22	1985-10-10	55992.73
78	Brandi	Cavendish	Jerald	BrandiCavendish0@gmail.com	044-623-94-48	1988-07-07	1982-08-02	50908.26
79	Ahmed	Kolthoff	Alfy	AhmedKolthoff0@gmail.com	044-676-58-34	1980-02-22	1982-08-03	55304.01
80	Dirk	Cochran	Domingo	DirkCochran0@gmail.com	044-385-56-93	1996-05-02	1995-04-08	33026.32
81	Daysi	Wadlinger	Angeli	DaysiWadlinger0@gmail.com	044-873-32-79	1994-04-12	1983-07-20	5531.39
82	Stan	Mordecai	Maurise	StanMordecai0@gmail.com	044-374-43-39	1985-09-23	1992-04-04	37545.83
83	Calista	Stransky	Jourdain	CalistaStransky0@gmail.com	044-644-11-27	1982-06-06	1983-04-10	14580.3
84	Lashon	Buice	Ezra	LashonBuice0@gmail.com	044-115-96-24	1992-06-07	1997-11-15	61813.62
85	Elizabet	Bunger	Mano	ElizabetBunger0@gmail.com	044-686-16-12	1994-11-13	1994-05-06	55485.63
86	Vannesa	Hintergardt	Ignace	VannesaHintergardt0@gmail.com	044-529-91-19	1982-04-10	1981-01-16	86360.66
87	Katy	Mettig	Mackenzie	KatyMettig0@gmail.com	044-114-36-45	1983-09-21	1993-03-17	6880.44
88	Quincy	Gleisner	Frants	QuincyGleisner0@gmail.com	044-672-98-36	1996-09-15	1995-10-04	33375.48
89	Jena	Venzke	Kellby	JenaVenzke0@gmail.com	044-335-48-43	1993-05-13	1992-06-17	61392.9
90	Woodrow	Bekins	Humphrey	WoodrowBekins0@gmail.com	044-179-90-68	1997-03-21	1980-10-13	12720.68
91	Belva	Conour	Frans	BelvaConour0@gmail.com	044-627-86-40	1988-09-12	1983-04-16	51592.37
92	Darrin	Stauffacher	Ashby	DarrinStauffacher0@gmail.com	044-947-18-71	1986-10-05	1993-03-05	20724.64
93	Fatima	Govia	Clevey	FatimaGovia0@gmail.com	044-283-75-26	1989-11-07	1983-01-08	49868.42
94	Antionette	Kalehuawehe	Bruce	AntionetteKalehuawehe0@gmail.com	044-701-41-11	1990-02-02	1988-09-17	6181.28
95	Geraldo	Mclawhorn	Lemmie	GeraldoMclawhorn0@gmail.com	044-366-23-17	1993-02-24	1987-05-16	23869.97
96	Fairy	Fracier	Tripp	FairyFracier0@gmail.com	044-823-55-76	1995-10-05	1995-07-03	30067.47
97	Dotty	Kerwood	Ferris	DottyKerwood0@gmail.com	044-346-56-45	1990-04-21	1988-09-05	53475.64
98	Emmitt	Horsman	Graig	EmmittHorsman0@gmail.com	044-655-42-52	1999-07-13	1993-05-05	2640.25
99	Harriette	Cimo	Haroun	HarrietteCimo0@gmail.com	044-114-57-25	1981-11-12	1980-01-19	55447.15
100	Evita	Strike	Aloin	EvitaStrike0@gmail.com	044-211-27-81	1983-03-09	1980-04-01	21653.72
101	Maryanna	Joern	Hale	MaryannaJoern0@gmail.com	044-411-98-70	1981-05-06	1985-11-04	71864.12
102	Lovie	Boufford	Alaster	LovieBoufford0@gmail.com	044-881-83-65	1990-11-08	1996-11-09	5374.56
103	Randal	Casmore	Asher	RandalCasmore0@gmail.com	044-122-70-78	1993-06-10	1993-04-08	7086.52
104	Leena	Hrycenko	Brand	LeenaHrycenko0@gmail.com	044-633-85-12	1991-09-07	1988-08-06	2407.53
105	Gigi	Orrantia	Maje	GigiOrrantia0@gmail.com	044-242-34-94	1986-07-17	1990-04-03	52645.4
106	Alaine	Guadian	Tobias	AlaineGuadian0@gmail.com	044-917-95-96	1980-06-24	1983-02-16	31856.74
107	Ruthanne	Canute	Beauregard	RuthanneCanute0@gmail.com	044-708-26-11	1996-01-13	1982-01-01	1082.75
108	Virgilio	Brookie	Kermit	VirgilioBrookie0@gmail.com	044-450-69-44	1992-09-02	1983-02-12	56131.23
109	Myesha	Boeneke	Stanford	MyeshaBoeneke0@gmail.com	044-987-62-24	1983-05-16	1980-11-08	13297.41
110	Norine	Reeds	Harlan	NorineReeds0@gmail.com	044-631-22-95	1992-10-07	1993-08-12	28677.97
111	Lucas	Masgalas	Ambrose	LucasMasgalas0@gmail.com	044-216-23-87	1990-02-02	1993-10-03	28944.42
112	Gricelda	Pound	Corty	GriceldaPound0@gmail.com	044-200-15-45	1981-02-08	1986-09-12	4549.69
113	Olympia	Gargani	Dominick	OlympiaGargani0@gmail.com	044-636-47-35	1986-05-05	1998-03-16	42264.27
114	Kira	Lorenzi	Gualterio	KiraLorenzi0@gmail.com	044-955-25-34	1986-05-13	1985-03-03	48252.12
115	Lesli	Slocumb	Fletch	LesliSlocumb0@gmail.com	044-254-53-71	1994-04-12	1983-09-20	3636.86
116	Sadie	Krage	Torrence	SadieKrage0@gmail.com	044-408-62-12	1999-10-15	1989-09-10	24260.52
117	Shantell	Blumenstein	Hoyt	ShantellBlumenstein0@gmail.com	044-928-73-61	1996-07-03	1987-10-12	42121.82
118	Gayla	Quarry	Lemmy	GaylaQuarry0@gmail.com	044-375-19-19	1980-06-01	1989-11-09	35620.45
119	Isaias	Limon	Corbin	IsaiasLimon0@gmail.com	044-921-44-87	1996-03-24	1982-04-14	51855.49
120	Robyn	Zaretsky	Donaugh	RobynZaretsky0@gmail.com	044-142-20-23	1999-05-15	1993-03-11	48812.25
121	Olimpia	Sabia	Huntley	OlimpiaSabia0@gmail.com	044-207-11-20	1992-02-12	1996-07-21	54420.09
122	Lashaunda	Dubon	Gibb	LashaundaDubon0@gmail.com	044-126-88-95	1998-08-07	1997-08-14	47688.64
123	Siobhan	Tufnell	Langsdon	SiobhanTufnell0@gmail.com	044-198-94-78	1982-03-04	1993-04-02	39289.18
124	Tuyet	Throneburg	Orlan	TuyetThroneburg0@gmail.com	044-453-41-55	1980-06-02	1995-05-21	9251.72
125	Jamison	Soltow	Reg	JamisonSoltow0@gmail.com	044-372-20-83	1988-07-24	1995-09-11	5960.91
126	Dion	Kulig	Wendel	DionKulig0@gmail.com	044-354-83-79	1999-10-22	1980-07-18	23336.46
127	Rosia	Redinger	Carmine	RosiaRedinger0@gmail.com	044-986-30-89	1987-09-09	1988-10-11	32830.13
128	Taisha	Cane	Malcolm	TaishaCane0@gmail.com	044-455-57-41	1986-07-07	1995-03-14	10868.95
129	Rolf	Pasley	ErvIn	RolfPasley0@gmail.com	044-428-90-44	1989-05-10	1994-01-03	52559.09
130	Neville	Mozie	Patrice	NevilleMozie0@gmail.com	044-789-79-12	1997-07-06	1986-05-09	26270.83
131	Eliz	Kennedy	Trever	ElizKennedy0@gmail.com	044-157-27-88	1995-07-04	1998-04-06	61050.02
132	Annabell	Dunnahoo	Delmar	AnnabellDunnahoo0@gmail.com	044-992-40-41	1988-10-24	1993-06-13	46241.81
133	Angel	Kinkead	Jayson	AngelKinkead0@gmail.com	044-531-15-75	1991-02-20	1986-05-22	17154.65
134	Ariane	Bendick	Walt	ArianeBendick0@gmail.com	044-276-67-37	1996-11-09	1988-03-18	80272.63
135	Narcisa	Sa	Weber	NarcisaSa0@gmail.com	044-334-85-72	1995-08-07	1997-08-06	288.1
136	Margie	Pavlikowski	Port	MargiePavlikowski0@gmail.com	044-605-23-84	1991-07-09	1984-03-01	7834.54
137	Vada	Youssef	Walden	VadaYoussef0@gmail.com	044-698-43-42	1991-04-21	1991-03-02	23357.97
138	Zena	Hizer	Yard	ZenaHizer0@gmail.com	044-414-91-36	1984-11-12	1984-11-04	14029.75
139	Jo	Scandalis	Stanislas	JoScandalis0@gmail.com	044-921-61-39	1992-09-05	1997-05-24	33933.4
140	Debera	Delegado	Tudor	DeberaDelegado0@gmail.com	044-871-55-64	1993-02-14	1985-02-24	38062.05
141	Ela	Gilgore	Fair	ElaGilgore0@gmail.com	044-468-72-54	1984-08-07	1986-07-20	49000.99
142	Alishia	Hauptman	Adan	AlishiaHauptman0@gmail.com	044-231-77-83	1995-03-24	1984-01-14	12210.49
143	Fidel	Teehan	Dael	FidelTeehan0@gmail.com	044-358-10-84	1987-02-21	1996-03-21	2377.33
144	Thad	Strasters	Andras	ThadStrasters0@gmail.com	044-551-27-15	1997-09-21	1980-08-24	89456.95
145	Lynelle	Bravata	Humphrey	LynelleBravata0@gmail.com	044-881-88-80	1983-07-24	1989-07-01	26479.94
146	Jaqueline	Schieber	Miles	JaquelineSchieber0@gmail.com	044-380-16-89	1985-08-05	1983-03-05	6062.2
147	John	Larney	Erasmus	JohnLarney0@gmail.com	044-142-23-34	1999-07-20	1990-03-11	37250.03
148	Janetta	Feyh	Iggy	JanettaFeyh0@gmail.com	044-216-18-26	1993-05-12	1981-06-15	6514.9
149	Serina	Nanna	Duncan	SerinaNanna0@gmail.com	044-388-58-68	1984-10-01	1980-09-02	7497.62
150	Veola	Newingham	Damiano	VeolaNewingham0@gmail.com	044-501-24-85	1981-03-21	1999-11-04	3314.28
151	Jessia	Nemes	Clare	JessiaNemes0@gmail.com	044-778-77-73	1998-05-22	1993-09-04	58176.32
152	Cody	Blalock	Lesley	CodyBlalock0@gmail.com	044-880-22-16	1995-05-16	1992-02-22	11178.65
153	Walton	Virant	Bartlet	WaltonVirant0@gmail.com	044-809-63-21	1993-06-01	1999-10-08	22087.91
154	Gloria	Abbs	Gustave	GloriaAbbs0@gmail.com	044-390-10-41	1984-10-23	1980-11-12	65810.41
155	Anh	Seit	Tobie	AnhSeit0@gmail.com	044-782-88-77	1995-08-24	1993-05-01	9336.25
156	Karie	Andrian	Garrott	KarieAndrian0@gmail.com	044-194-19-31	1981-02-06	1996-05-11	5241.59
157	Crystal	Zendejas	Bret	CrystalZendejas0@gmail.com	044-892-17-85	1988-06-06	1997-06-19	13951.99
158	Carly	Gruhlke	Alano	CarlyGruhlke0@gmail.com	044-308-80-33	1984-04-06	1998-01-18	34061.55
159	Shoshana	Nakata	Cos	ShoshanaNakata0@gmail.com	044-148-49-34	1985-09-12	1988-11-07	31060.52
160	Belkis	Ashbach	Mord	BelkisAshbach0@gmail.com	044-119-34-28	1996-06-11	1983-03-01	47917.96
161	Marguerita	Simonian	Isaac	MargueritaSimonian0@gmail.com	044-344-16-54	1984-01-04	1997-03-06	7023.79
162	Eliz	Bevelle	Rossie	ElizBevelle0@gmail.com	044-410-39-46	1995-06-15	1983-05-05	14526.22
163	Enoch	Ned	Jessey	EnochNed0@gmail.com	044-775-78-75	1998-04-23	1981-08-15	26142.36
164	Delorse	Barmes	Daven	DelorseBarmes0@gmail.com	044-534-50-29	1980-07-05	1990-09-05	60392
165	Lorrine	Correy	Rob	LorrineCorrey0@gmail.com	044-388-46-52	1988-04-16	1999-01-18	15037.94
166	Devin	Waisman	Rancell	DevinWaisman0@gmail.com	044-161-28-83	1980-03-15	1985-01-12	198.93
167	Isabell	Edmunson	Fielding	IsabellEdmunson0@gmail.com	044-790-31-38	1988-04-02	1982-09-24	3145.36
168	Suzanne	Kreutzbender	Arin	SuzanneKreutzbender0@gmail.com	044-119-95-92	1993-10-14	1984-02-04	39266.71
169	Mariel	Froid	Isaak	MarielFroid0@gmail.com	044-364-57-11	1998-11-04	1990-05-08	54854.3
170	Milagros	Placker	Kipp	MilagrosPlacker0@gmail.com	044-675-76-17	1981-06-13	1987-08-12	80799.2
171	Rob	Blackson	Mohandas	RobBlackson0@gmail.com	044-423-23-60	1998-10-09	1993-10-16	50704.06
172	Shawnee	Colinger	Amery	ShawneeColinger0@gmail.com	044-795-82-95	1984-04-10	1997-11-19	20175.36
173	Yuk	Stampley	Michale	YukStampley0@gmail.com	044-213-41-29	1992-07-02	1988-11-05	5061.92
174	Nikki	Louer	Adham	NikkiLouer0@gmail.com	044-927-81-41	1994-08-03	1999-01-24	28464.14
175	Concha	Himmel	Dennison	ConchaHimmel0@gmail.com	044-477-83-81	1999-10-05	1998-02-18	59408.38
176	Geneva	Teakell	Mac	GenevaTeakell0@gmail.com	044-926-63-68	1998-10-18	1987-09-21	4965.97
177	Sherlyn	Bone	Billy	SherlynBone0@gmail.com	044-516-20-52	1997-09-09	1984-01-02	68662.19
178	Jennifer	Mestad	Perren	JenniferMestad0@gmail.com	044-741-75-65	1991-03-12	1980-04-23	50026.9
179	Pasquale	Hager	Eugene	PasqualeHager0@gmail.com	044-132-49-17	1993-01-21	1989-01-14	7246.96
180	Floretta	Neshem	Yule	FlorettaNeshem0@gmail.com	044-912-73-80	1986-04-17	1997-10-08	5277.11
181	Anna	Puehler	Randie	AnnaPuehler0@gmail.com	044-970-56-34	1990-04-21	1991-02-14	49722
182	Ela	Walat	Freddy	ElaWalat0@gmail.com	044-489-81-61	1983-07-12	1991-06-06	11743.29
183	Monty	Granstrom	Sylvan	MontyGranstrom0@gmail.com	044-677-66-77	1992-06-24	1992-06-24	9304.34
184	Kenny	Giarrano	Kinnie	KennyGiarrano0@gmail.com	044-468-89-65	1993-05-19	1988-02-03	15224.88
185	Brandon	Zazozdor	Tanner	BrandonZazozdor0@gmail.com	044-835-18-70	1989-08-15	1982-10-02	33909.54
186	Nu	Ryckman	Raimundo	NuRyckman0@gmail.com	044-211-36-91	1980-06-06	1995-03-20	36011.98
187	Shay	Matthies	Giulio	ShayMatthies0@gmail.com	044-401-79-16	1999-04-04	1995-03-02	2683.01
188	Patria	Bisesi	Stephanus	PatriaBisesi0@gmail.com	044-704-62-62	1995-10-07	1986-06-06	16103.61
189	Norma	Tocher	Krishnah	NormaTocher0@gmail.com	044-403-89-85	1993-08-05	1982-03-15	37412.1
190	Rhona	Ruschel	Sherlock	RhonaRuschel0@gmail.com	044-804-16-24	1991-05-16	1999-02-22	51087.81
191	Felisa	Spoonamore	Seumas	FelisaSpoonamore0@gmail.com	044-608-97-70	1997-11-19	1998-06-11	4859.79
192	Nicole	Hidde	Tyler	NicoleHidde0@gmail.com	044-500-35-71	1990-04-13	1993-03-14	19856.04
193	Kizzy	Lotz	Mal	KizzyLotz0@gmail.com	044-478-15-68	1994-07-09	1991-06-20	29026.45
194	Lourie	Kordiak	Con	LourieKordiak0@gmail.com	044-192-77-47	1980-09-05	1990-01-04	15968.4
195	Argentina	Alshouse	Dag	ArgentinaAlshouse0@gmail.com	044-305-34-77	1981-07-13	1994-08-11	32110.46
196	Aide	Sessom	Vito	AideSessom0@gmail.com	044-705-67-43	1986-06-08	1986-05-06	68446
197	Jeff	Rousselle	Carlyle	JeffRousselle0@gmail.com	044-902-27-34	1988-05-04	1995-10-03	69996.07
198	Vi	Bayona	Ric	ViBayona0@gmail.com	044-589-29-21	1984-06-18	1993-05-19	5427.64
199	Eric	Annarummo	Wilmer	EricAnnarummo0@gmail.com	044-422-61-38	1983-06-04	1995-11-22	54927.79
200	Griselda	Troublefield	Cletis	GriseldaTroublefield0@gmail.com	044-251-29-86	1994-05-01	1981-09-18	28416.45
\.


--
-- Data for Name: positions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.positions (id, name) FROM stdin;
1	Executive
2	Manager
3	Accountant
4	Customer service
5	Sales
6	Advisor
7	Developer
8	President
\.


--
-- Name: department_position_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.department_position_id_seq', 200, true);


--
-- Name: departments_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.departments_id_seq', 7, true);


--
-- Name: employees_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.employees_id_seq', 200, true);


--
-- Name: positions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.positions_id_seq', 8, true);


--
-- Name: department_position department_position_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_position
    ADD CONSTRAINT department_position_pkey PRIMARY KEY (id);


--
-- Name: departments departments_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_pkey PRIMARY KEY (id);


--
-- Name: employees employees_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (id);


--
-- Name: positions positions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.positions
    ADD CONSTRAINT positions_pkey PRIMARY KEY (id);


--
-- Name: department_position department_position_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_position
    ADD CONSTRAINT department_position_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.departments(id);


--
-- Name: department_position department_position_employee_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_position
    ADD CONSTRAINT department_position_employee_id_fkey FOREIGN KEY (employee_id) REFERENCES public.employees(id);


--
-- Name: department_position department_position_position_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_position
    ADD CONSTRAINT department_position_position_id_fkey FOREIGN KEY (position_id) REFERENCES public.positions(id);


--
-- PostgreSQL database dump complete
--

