# TPSGame
 
조작키
  - 이동 : w a s d
  - 점프 : space
  - 달리기 : left shift

Manager를 통한 동적 할당과 player state machine을 구현하기 위한 프로젝트.

동적 할당
   - Managers 에서 다른 매니저 스크립트들을 불러와서 싱글톤화 시켜줌.
   - [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]를 사용해서 Scene에 오브젝트가 없어도 Managers를 동적할당 해준다.

player state machine
   - Rigidbody를 사용해서 움직임을 제어하고자 함.
   - BlendTree를 사용해서 애니메이션을 제어함.
   - Slope처리가 되지 않아, 캐릭터가 날아가는 경우가 있음.
