# Huyền Thoại Nón Lá

Một project game 2D được xây dựng bằng **Unity** với bối cảnh phiêu lưu hành động.  
Người chơi điều khiển nhân vật, di chuyển qua màn chơi, chiến đấu với quái và chạm điểm đích để chiến thắng.

> Project hiện vẫn đang trong quá trình phát triển.

---

# Giới thiệu

**Huyền Thoại Nón Lá** là một game 2D side-scrolling được phát triển bằng Unity.  
Game mang phong cách phiêu lưu hành động, nơi người chơi điều khiển nhân vật vượt qua các thử thách và chiến đấu với kẻ địch.

Trong game, người chơi sẽ:

- Di chuyển qua các màn chơi
- Tránh hoặc tiêu diệt kẻ địch
- Sử dụng kỹ năng chiến đấu
- Hoàn thành màn chơi để chiến thắng

---

# Công nghệ sử dụng

Project được phát triển bằng:

- **Unity Engine**
- **C#**
- **Unity 2D Physics**
- **Animator**
- **Tilemap**
- **AudioSource / AudioClip**

---

# Cấu trúc thư mục

```
Assets/
│
├── Animation/              # Animation của nhân vật và enemy
├── Audio/                  # Âm thanh game
├── Prefab/                 # Prefab object
├── Scenes/                 # Scene của game
├── StoneUI/                # UI assets
├── Tile Map/               # Map tile
├── forest-nature-fantasy-tileset-free/  # Tileset map
│
├── Game Manager.cs         # Quản lý trạng thái game
├── PatrolEnemy.cs          # Logic enemy
├── Player.cs               # Logic player
└── SceneManagerMent.cs     # Chuyển scene
```

Ngoài ra Unity project còn có các thư mục mặc định:

```
Packages/
ProjectSettings/
```

---

# Gameplay hiện có

## Player

Nhân vật người chơi hiện có các chức năng:

- Di chuyển trái phải
- Nhảy
- Tấn công cận chiến
- Nhận sát thương
- Chết khi hết máu
- Hoàn thành màn khi chạm điểm chiến thắng

---

## Enemy

Enemy có các hành vi:

- Tuần tra tự động
- Phát hiện người chơi
- Đuổi theo player
- Tấn công khi ở gần
- Nhận sát thương
- Chết khi máu về 0

---

## Game Flow

Game sử dụng các script chính:

- **GameManager**  
  Quản lý trạng thái game thông qua biến `isGameActive`.

- **SceneManagerMent**  
  Chuyển scene từ menu vào màn chơi.

---

# Điều khiển

Các phím điều khiển cơ bản:

| Phím | Chức năng |
|-----|-----------|
| A / ← | Di chuyển trái |
| D / → | Di chuyển phải |
| Space | Nhảy |
| Chuột trái | Tấn công |

---

# Cách chạy project

## 1 Clone project

```bash
git clone https://github.com/hoaikhoitran/huyen-thoai-non-la.git
```

---

## 2 Mở bằng Unity Hub

1. Mở **Unity Hub**
2. Chọn **Add Project**
3. Chọn thư mục project vừa clone

---

## 3 Mở scene game

Vào:

```
Assets/Scenes
```

Sau đó mở scene:

```
Lv1
```

---

## 4 Chạy game

Nhấn nút **Play** trong Unity Editor để chạy game.

---

# Các script chính

## Player.cs

Script điều khiển nhân vật:

- Movement
- Jump
- Attack
- Health system
- Death
- Win / Lose
- Sound effect

---

## PatrolEnemy.cs

Script điều khiển enemy:

- Tuần tra
- Raycast kiểm tra mép đất
- Phát hiện player
- Đuổi theo
- Attack
- Nhận damage
- Chết

---

## Game Manager.cs

Script quản lý trạng thái game.

---

## SceneManagerMent.cs

Script chuyển scene trong game.

---

# Tính năng dự kiến

Trong tương lai project có thể phát triển thêm:

- UI thanh máu đẹp hơn
- Thêm nhiều loại enemy
- Boss fight
- Hệ thống điểm
- Coin / item
- Nhiều màn chơi hơn
- Menu settings
- Save game

---

# Screenshot

Bạn có thể thêm ảnh gameplay vào đây.

Ví dụ:

```
![Gameplay](Assets/Screenshot.png)
```

---

# Trạng thái project

🚧 **Work In Progress**

Project hiện đang được phát triển và chưa hoàn thiện.

---

# Tác giả

GitHub:  
https://github.com/hoaikhoitran

---

# License

Chưa có license cụ thể.  
Bạn có thể thêm file `LICENSE` nếu muốn public project.
