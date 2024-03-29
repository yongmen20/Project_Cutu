# CutuGirl

뱀파이어서바이벌 모작게임입니다.  

## 목차
- [개요](#개요)  
- [게임설명](#게임설명)
- [게임플레이](#게임플레이)
- [플레이영상](#플레이영상) 

## 개요
- 프로젝트 이름 : CutuGirl
- 프로젝트 개발기간 : 2021.09 - 2021.11
- 개발 엔진 및 언어 : Unity & C#
- 기타 프로그램 : Spine
- 맴버 : 남기산

## 게임설명

|![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/479e676d-6cdf-4bbb-86a7-04f077239510)|![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/0fb77d91-fe05-4ff7-a3e3-a22f833c3806)|
|---|---|
|<p align="center">메인화면|<p align="center">플레이화면|  

Unity2D를 통해 개발한 간단한 뱀파이어서바이벌 모작게임입니다.  
object pooling기능을 이용하여 다수의 적을 생성하며 일정 경험치를 모아 레벨업을 하는 로직을 가지고 있는 프로그램입니다.  
제한시간은 10분이며 1분마다 점점 더 강한 적들이 출현하며, 플레이어는 제한시간동안 살아남으면 승리합니다.

### 게임 규칙
|![KakaoTalk_20240118_135121527](https://github.com/yongmen20/Project_Cutu/assets/148856359/bebd7c4d-8bec-4b9e-af21-8a5bbbb58c60)|![KakaoTalk_20240118_135121527_01](https://github.com/yongmen20/Project_Cutu/assets/148856359/ded5b867-524e-47a0-804e-cc8952cf5f82)|![KakaoTalk_20240118_135121527_02](https://github.com/yongmen20/Project_Cutu/assets/148856359/a652a692-32d4-4037-9057-1ebb832c4e7c)|
|---|---|---|
|<p align="center">플레이어 조작|<p align="center">게임클리어|<p align="center">게임오버|

### **플레이어 조작**
---
1. **플레이어는 방향키/WASD 버튼을 통해 맵의 x/y축을 자유롭게 이동할 수 있습니다.**  
  1-1. 플레이어는 한정된 맵 안에서만 이동할 수 있습니다.  
  1-2. 플레이어는 맵에 배치된 장애물(나무)을 통과할 수 없지만 적은 통과할 수 있습니다.  
  1-3. 플레이어는 적을 통과할 수 없고 충돌이 일어나면 체력이 감소합니다.
   
2. **플레이어는 Space 버튼을 통해 현재 이동중인 방향으로 빠르게 대시할 수 있습니다.**  
  2-1. 대시에는 쿨타임이 존재하며 연속적으로 사용할 수 없습니다.  
  ㅤ2-1-1. 대시 쿨타임은 화면 아래 체력바 아래에 표시되며 대시가 가능하면 파란색 바가 점등하며 쿨타임 동안은 소등됩니다.  
  2-2. 대시 기능을 사용하는 중에는 이동을 제어할 수 없습니다.  
  2-3. 대시를 사용하더라도 적과의 충돌은 여전히 존재합니다.  
  ㅤ2-3-1. 플레이어는 몬스터보다 질량이 높기 때문에 대시를 사용하면 적을 밀쳐낼 수 있습니다.  
  2-4. 대시를 사용할 때 맵에 배치된 장애물(나무)을 통과할 수 없습니다.  
   
3. **플레이어는 마우스로 적을 공격하거나 선택을 할 수 있다.**  
  3-1. 플레이어는 마우스로 방향을 지정해 공격할 수 있다.  
ㅤㅤ3-1-1. 해당 설명은 기본 원거리 공격에만 해당한다. 공격은 일정한 간격으로 자동으로 발사된다.  
  3-2. 플레이어는 레벨업 화면에서 아이콘을 클릭해 새로운 능력을 획득하거나 강화할 수 있다.  
ㅤㅤ3-2-1. 만약 플레이어가 모든 능력을 업그레이드했다면 체력회복 아이콘만 출력한다.  
   
4. **플레이어는 ESC키를 통해 게임을 일시정지할 수 있습니다.**  
  4-1. 일시정지 상태에서 다시 ESC키를 입력하면 다시 게임이 실행됩니다.

### **게임진행**
---
1. **제한시간은 10분이며 1분마다 강화된 적이 출현합니다(레벨시스템).**  
   1-1. 제한시간이 2분이 되면 일반 적들보다 빠른 보스가 출현합니다.  
   
### **게임클리어**
---
1. **플레이어는 제한시간(10분)동안 생존하면 화면내의 모든 적을 삭제하고 결과창을 출력합니다.**  
  1-1. 결과창에는 최종 점수를 출력하고 메뉴화면으로 복귀하는 버튼을 생성합니다.  

### **게임오버**
---
1. **플레이어가 제한시간(10분)동안 생존하지 못하면 특정 애니메이션과 함께 게임을 정지시키고 결과창을 출력합니다.**  
  1-1. 결과창에는 최종 점수를 출력하고 메뉴화면으로 복귀하는 버튼을 생성합니다.  

## 게임플레이
- **플레이어 조작**
---
  
|이동|상|하|좌|우|
|---|---|---|---|---|
|<p align="center">키보드|<p align="center">W|<p align="center">S|<p align="center">A|<p align="center">D|  

- **UI**
---
1. 메뉴화면  
![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/ae094147-ed73-4c3a-a14d-5a638ee04260)  

2. 게임화면  
![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/8ee96b6f-e60a-4508-a4d4-66825de9f633)  

3. 일시정지화면  
![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/90665793-1c55-4f9b-ab5e-85be482539a2)  

4. 결과화면  
![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/c5db0790-15ae-489a-bc38-22cc61169c5c)  

- **설정값**
---
![CthuluMagicGirl-Idle_00](https://github.com/yongmen20/Project_Cutu/assets/148856359/09c5ff1d-8ab1-408e-8eb4-deca3272c69c)  
**플레이어**
  
|속성|플레이어(Tag:Player)|
|---|---|
|<p align="center">체력|<p align="center">70|
|<p align="center">속도 / 증가폭|<p align="center">1.3 / 2~5%|
|<p align="center">대시속도 / 지속시간 / 쿨타임|<p align="center">7 / 2초 / 1초|
|<p align="center">근접공격력 / 증가폭|<p align="center">7.5 / 50%|
|<p align="center">원거리공격력 / 증가폭|<p align="center">15 / 5%|
|<p align="center">유도공격력 / 증가폭|<p align="center">10 / 2~5%|

![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/60bca509-b98f-4517-9073-a02c97b65f83)  
**일반적**
  
|속성|적(Tag:Enemy)|
|---|---|
|<p align="center">체력|<p align="center">10~270|
|<p align="center">속도|<p align="center">0.5~0.7|
|<p align="center">공격력|<p align="center">1|

![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/c4303e37-0dee-47d0-85ab-c74fc19d723f)  
**보스**

|속성|적(Tag:Enemy)|
|---|---|
|<p align="center">체력|<p align="center">10000|
|<p align="center">속도|<p align="center">1.5|
|<p align="center">공격력|<p align="center">1|

![image](https://github.com/yongmen20/Project_Cutu/assets/148856359/13b83f18-bd08-4f3a-a4d6-d6c2e793e623)  
**아이템박스**

|속성|적(Tag:Box)|
|---|---|
|<p align="center">체력|<p align="center">30|  

## 플레이영상
--- 
https://github.com/yongmen20/Project_Cutu/assets/148856359/c8ba9532-395f-47af-bebd-0e83f7762002


  
- **참고자료**
--- 
골드메탈님의 유니티 기초 뱀서라이크🧟언데드서바이버 강의를 참고하였습니다.  
https://www.youtube.com/watch?v=MmW166cHj54&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x


  
