# CutuGirl

뱀파이어서바이벌 모작게임입니다.  

## 목차
- [개요](#개요)  
- [게임설명](#게임설명)
- [게임플레이](#게임플레이)  

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
   
3. **플레이어는 마우스를 통해 적을 조준할 수 있다.**  

### **게임클리어**


### **게임오버**


## 게임플레이
- 플레이어 조작
  
|이동방향|좌(LEFT)|우(RIGHT)|공격(ATTACK)|
|---|---|---|---|
|<p align="center">키보드|<p align="center">A|<p align="center">D|<p align="center">Z|

- 플레이 영상
  


골드메탈님의 유니티 기초 뱀서라이크🧟언데드서바이버 강의를 참고하였습니다.  
https://www.youtube.com/watch?v=MmW166cHj54&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x


  
