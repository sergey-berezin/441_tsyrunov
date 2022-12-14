# **Task_3**
**WPF-приложение**, использующее NuGet-пакет, создаваемый библиотекой **Task_1**, предоставляющее пользовательский интерфейс с использованием модели машинного обучения для определения вероятности эмоций на изображении, а также базу данных (**Proccessed_Images.db**) для хранения результатов обработки. (О формировании пакета и работы с ним см. в файле [README](https://github.com/tsirleo/Lab_DotNET/blob/master/Task_1/README.md) внутри соответствующего каталога)

## **Для запуска программы:** 
**1. Перейти в директорию Task_1/ModelLib:**
```
cd ../Task_1/ModelLib
```
**2. Сформировать пакет NuGet:**
```
dotnet pack
```
**3. Перейти в директорию Task_3/WPFApp:**
```
cd ../../Task_3/WPFApp
```
**3. Установить dotnet-ef:**
```
dotnet tool install --global dotnet-ef
```
**4. Обновить dotnet-ef:**
```
dotnet tool update --global dotnet-ef
```
**5. Проверить корректность установки dotnet-ef:**
```
dotnet ef
```
Пример выходных данных:
```
                     _/\__
               ---==/    \\
         ___  ___   |.    \|\
        | __|| __|  |  )   \\\
        | _| | _|   \_/ |  //|\\
        |___||_|       /   \\\/\\
Entity Framework Core .NET Command-line Tools 2.1.3-rtm-32065
<Usage documentation follows, not shown.>
```
**6. Добавить миграции в проект:**
```
dotnet ef migrations add InitialCreate
```
**7. Создать базу данных и схему из миграции:**
```
dotnet ef database update
```
**8. Запустить приложение:**
```
dotnet run
```
#### Удаление ресурсов можно выполнить командами:
```
dotnet ef database drop
dotnet ef migrations remove
```
#### В случае возникновения ошибки "SQLite Error 1: no such table: [tablename]":
Установить рабочий каталог:
- В **Обозревателе решений** щелкнуть проект правой кнопкой мыши и выбрать пункт **Свойства**.
- Выберать вкладку **Отладка** на левой панели.
- Установить **Рабочий каталог** в каталог проекта.
- Сохранить изменения.

## **Условие:** 
Требуется: сохранять результаты работы программы из задания [Task_2](https://github.com/tsirleo/Lab_DotNET/tree/master/Task_2) в постоянном хранилище (persistent storage). При обработке очередного изображения проверять его наличие в хранилище, и выполнять анализ изображения только в случае отсутствия. В хранилище записывается каждое изображение и вероятности эмоций для него.  Приложение при старте показывает содержимое хранилища аналогично заданию [Task_2](https://github.com/tsirleo/Lab_DotNET/tree/master/Task_2).  В приложении предусмотрена возможность удалить любое из записанных ранее изображений. 

### **Требования к реализации:** 
- Для организации постоянного хранилища применяется технология Entity Framework или Entity Framework Core (можно выбирать любую).  
- Содержимое изображений должно храниться в блобах в базе данных. При поиске по хэш-коду или ограничивающему прямоугольнику содержимое блоба из базы данных загружаться не должно. 
- Все отношения базы данных должны быть в третьей нормальной форме. 
- С целью ускорения поиска дубликаты ищутся по хэш-коду массива байт файла.  Если совпадения найдены, то сверяется полное содержимое файла. 