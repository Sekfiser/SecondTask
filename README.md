# SecondTask
При запуске пересоздает таблицы в базе данных, поэтому после 1го запуска надо закомментить строку Database.EnsureDeleted(); в файле ApplicationContext </br>
Сам параметризированный запрос в файле Index.cshtml.cs </br>
Подключался к mysql 8.0.30 запущенную в openserver. Файл ApplicationContext содержит путь подключения. </br>
Запрос скорее всего не оптимальный, но из-за специфики работы group by пришлось использовать mysql и команду SET sql_mode =''; </br>
