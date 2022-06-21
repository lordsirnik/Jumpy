using System.Collections;
using System.Collections.Generic;
using Jumpy;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestScripts
{

	[UnitySetUp]                         //Метод,в котором делаем Предусловия для тестов ниже
	public IEnumerator Setup()           //Название этого теста
	{
		SceneManager.LoadScene("Game");  //Обращаемся к SceneManager - родительскому классу загрузки сцен

		yield return null;                // Пропускаем два кадра, чтобы дождаться загрузки сцены
		yield return null;                // Второй кадр
		
	}
	
	[TearDown]                            //Метод,в котором делаем Постусловие. То есть то, что будет выполняться ПОСЛЕ тестов
	public void Teardown()                //Наш метод пустой, так как мы не используем пост-условие в этих тестах
	{
	}
	
    [Test] //Выполняем простой тест
    public void AllManagerComponentsAddsOkay() //Название этого теста
    {
	    // Ищем объект  GameManager на сцене. Запоминаем его в переменной managerObject
	    var managerObject = Object.FindObjectOfType<GameManager>();
	    
	    // GameManager должен загрузить все следуюшие компоненты (так работает логика загрузки сцен в этом проекте):
	    Assert.IsNotNull(managerObject.GetComponent<UserInputManager>());     //Делаю Утверждение, что объект не отсутствует
	    Assert.IsNotNull(managerObject.GetComponent<AssetsLoaderManager>());  //Делаю Утверждение, что объект не отсутствует
	    Assert.IsNotNull(managerObject.GetComponent<SoundLoaderManager>());   //Делаю Утверждение, что объект не отсутствует
	    Assert.IsNotNull(managerObject.GetComponent<TweenManager>());         //Делаю Утверждение, что объект не отсутствует
	    
	    // Можно навести на каждый из компонентов и перейти на соответствующий скрипт, который выпадает в подсказке
    } 

    [UnityTest] //Создаем новый тест
    public IEnumerator CheckGameStartOnStartButton()                   //Проверяем что игра стартует по кнопке Start
    {
	    Time.timeScale = 20.0f;                                        // Ставим ускорение игрового времени в 20 раз, чтобы тест был быстрее
	    var titleScreen = Object.FindObjectOfType<TitleScreenPopup>(); //Находим заголовок попапа
	    
	    Assert.IsNotNull(titleScreen);                                 //Делаем утверждение, что заголовок попапа не пустой

	    var startButton = GameObject.Find("PlayButton");     // Находим кнопку PlayButton
	    
	    Assert.IsNotNull(startButton);                                 //Делаем утверждение, что кнопка находится на экране

	    titleScreen.PerformClickActionsPopup(startButton);             // Нажимаем на кнопку startButton

	    var time = 0f;                               //Задаю переменную time с типом float
	    while (time < 5f)                            // Цикл будет выполняться пока переменная time не пройдет 5 раз...
	    {
		    time += Time.fixedDeltaTime;             //... за каждый кадр.
													 
		    yield return new WaitForFixedUpdate();   // Пропустим кадр, пока не дойдем до 5
		                                             // Мы пропускаем время, чтобы все анимации успели прогрузиться на разных устройствах
                                                     // Time.DeltaTime стандартный класс юнити:
		                                             // https://docs.unity3d.com/Manual/TimeFrameManagement.html 
	    }
	    Time.timeScale = 1f;                         // Когда циул выполнится, устанавливаем скейл времени кратный единице(нормальный)

	    var inGameInterface = Object.FindObjectOfType<InGameInterface>(true); 
	                                                //Нахожу на сцене объект InGameInterface - это начало игры
	    
	    Assert.IsNotNull(inGameInterface);                    //Делаю утверждение что игровой интерфейс загрузился
	    Assert.IsTrue(inGameInterface.gameObject.activeSelf); //Делаею утверждение, что игровой интерфейс активен
    }
}
