insert into Articles values ('c2b59be2-8942-4bc8-9edf-024ffae28368','25-12-2017','Короткий гайд о том как по праволавному пилить приложения на WPF. EEE boy!!!','Работа с WPF',5,'С#')
insert into Steps values (1,'Ну что бы начать надо начать. Начинать сложнее всего, потому, что я честно говоря вообще хз как это делается, потому, что я никогда не начинал. Хотя я и никогда и не заканчивал. Всё бросал на пол пути. Да, вот такой я вот пёсик-болик','Начало работы с WPF')
insert into Steps values (1,'Так короче. Что бы установить С# вам понадобится: ..... . Когда вы его установите то можите знать, что C# вы установили. Но если вы его не установили, то знаете вы его не установили и вам теперь с этим жить. Я бы на вашем мест, что-нибудь с этим делал. Так как дальше так продолжаться не может. Это Грех как сказала бы моя бабушка.','Установка С#')
insert into Comments values ('c2b59be2-8942-4bc8-9edf-024ffae28368',1,'Что это вообще такое. Это что шедевр? Ещкере','26-12-2017',1);
insert into Comments values ('9a7bdf53-a482-409e-8c20-00746227682c',3,'Эщкере. Я никогда не читал такой прекрастной статьи.','27-12-2017',1);
insert into CommentLikeUser values (4,'c2b59be2-8942-4bc8-9edf-024ffae28368');
insert into CommentLikeUser values (4,'9a7bdf53-a482-409e-8c20-00746227682c');
insert into CommentLikeUser values (4,'c9c50413-6f02-482f-aeba-334149281736');
insert into CommentLikeUser values (7,'9a7bdf53-a482-409e-8c20-00746227682c');
insert into CommentLikeUser values (7,'c9c50413-6f02-482f-aeba-334149281736');
select * from CommentLikeUser
select * from AspNetUsers
select * from Articles;
select * from Steps;
select * from Comments; 