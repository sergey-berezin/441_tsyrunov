# **Task_5**
**Сервер, WPF-клиент и Web-клиент**:
- ***Сервер*** проводит проводит обработку изображений и их хранение, с использованием модели машинного обучения (***NuGet-пакет***, создаваемый библиотекой **Task_1**) для определения вероятности эмоций на изображении, а также базу данных (**Proccessed_Images.db**). Добавлена поддержка OpenApi и CORS.
- ***WPF-клиент*** - настольное приложение, которое с помощью http-запросов общается с ***Сервером***. WPF-клиент не генерируется из .json-файла из-за ошибки. Общение клиента с сервером происходит с использованием классов библиотеки [***Contracts***](https://github.com/tsirleo/Lab_DotNET/blob/master/Task_4/Contracts/DatabaseClasses.cs).
- ***Web-клиент*** - браузерное приложение написанное с использованием библиотеки **React**. Имеет ограниченный функционал по сравнению с настольным клиентом: доступно только отображение изображений и дополнительной информации о них, доступные методы ***GET*** и ***DELETE***. Для остального используется настольный клиент.  

О формировании NuGet-пакета и работe с ним см. в файле [README](https://github.com/tsirleo/Lab_DotNET/blob/master/Task_1/README.md) внутри соответствующего каталога.

Про работу с базой данных и работе с ней см. в файле [README](https://github.com/tsirleo/Lab_DotNET/blob/master/Task_3/README.md).

Про работу Сервера и WPFClient см. в файле [README](https://github.com/tsirleo/Lab_DotNET/blob/master/Task_4/README.md).

## **Генерация клиента**
Генарция файла клиента происходит с использованием пакета **[swagger-react-generator](https://www.npmjs.com/package/swagger-react-generator)**

**Для установки пакета выполнить команду:**
```
npm i swagger-react-generator
```
**Для генерации:**
1. Перейти в директорию Web-клиента:
    ```
    cd Task_5/Web_Client
    ```
2. Выполнить команду
    ```
    node node_modules/swagger-react-generator -I swagger.json -O /src/api.js
    ```
## **Для запуска Сервера и WPF-клиента:** 
**1. Перейти в директорию Task_1/ModelLib:**
```
cd Task_1/ModelLib
```
**2. Сформировать пакет NuGet:**
```
dotnet pack
```
**3. Перейти в директорию Task_5/Server:**
```
cd ../../Task_5/Server
```
**4. Добавить миграции в проект:**
```
dotnet ef migrations add InitialCreate
```
**5. Создать базу данных и схему из миграции:**
```
dotnet ef database update
```
**6. Запустить сервер:**
```
dotnet run --launch-profile "https"
```
**7. Перейти в директорию Task_5/WPFApp_Client:**
```
cd ../WPFApp_Client
```
**8. Запустить настольное клиентское приложение:**
```
dotnet run
```
## **Для запуска Web-клиента:**
Для запуска необходим стандартный диспетчер пакетов Node.js - [npm](https://www.npmjs.com/package/npm)

**1. Перейти в директорию Task_5/Web_Client:**
```
cd ../Web_Client
```
**2. Запустить браузерное клиентское приложение:**
```
npm start
```

## **Условие:** 
Требуется: создать работающий в веб-браузере пользовательский интерфейс для взаимодействия с сервисом распознавания изображений. Для доступа к сервису используется Web API из [задачи 4](https://github.com/tsirleo/Lab_DotNET/tree/master/Task_4). Расширение Web API из задачи 4 допустимо.

### **Требования к реализации:** 

- Способ отображения в веб-браузере информации аналогичен [заданию 4](https://github.com/tsirleo/Lab_DotNET/tree/master/Task_4).

- Веб-приложение должно позволять удалять из базы данных существующие изображения. Для пополнения базы данных используется настольное приложение из [задания 4](https://github.com/tsirleo/Lab_DotNET/tree/master/Task_4). 