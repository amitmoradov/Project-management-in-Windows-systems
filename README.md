## Bonus Features:

- **Event Trigger:**
  - [**Location:** Admin panel display layer](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/449e20e58018429d1056060494a47e66b112cd5d/dotNet5784_7061_3114/PL/MainWindow.xaml#L21)
  - **Functionality:** Activated by the "Manage Engineers List" button

- **Data Trigger:**
  - [**Location:** Task window display layer](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/0de0126296490bd5b4106e90d5bca6ff3fe2b036/dotNet5784_7061_3114/PL/Task/SingleTaskWindow.xaml#L121)
  - **Functionality:** Activated in the "Engineer Allocation" field

- **Feature Trigger:**
  - [**Location:** Engineer window display layer](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/361bc5e3af898898dcb07129411c43ee2df54a0e/dotNet5784_7061_3114/PL/ADMIN/Admin.xaml#L23)
  - **Functionality:** Concerns the engineer's price field

- **Clock Restoration:**
  - [**Location:** Project implementation interface](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/02270b866cab6120596178a5668fa5219aec8ac2/dotNet5784_7061_3114/DalXml/ProjectImplementation.cs#L65)
  - **Functionality:** Utilizes "virtual time" functions, triggering the first layer and saving data in files

- **Colors in Gantt:**
  - [**Location:** Converters in PL . ](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/7eed2777c0706f53d6ab5082d0b1c9974fba3af6/dotNet5784_7061_3114/PL/Converters.cs#L246)
  - **Functionality:** Uses a dictionary to apply colors

- **Automatic Schedule:**
  - [**Location:** Bl -> Task Implementation . ](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/45757d8186d7d3c90f6be8e200d9731299c223be/dotNet5784_7061_3114/BL/BlImplementation/TaskImplementation.cs#L289)
  - **Functionality:** Automates the scheduling processes

- **Window Style:**
  - [**Location:** Third layer, specifically in `App.xaml](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/6e2e11ccc2fae43d49f5cc690c70e04024bf5551/dotNet5784_7061_3114/PL/App.xaml#L17)
  - **Functionality:** Adjusts the visual style of windows

- **Graphic (Cost):**
  - [**Location:** Individual engineer window, in the cost field](https://github.com/amitmoradov/dotNet5784_7061_3114/blob/e3ce614f790435caa0b959db0d3023c11a7a36ce/dotNet5784_7061_3114/PL/Engineer/SingleEngineerWindow.xaml#L75)
  - **Functionality:** Displays cost-related graphics

- **Shape (Triangle):**
  - **Location:** Admin window
  - **Functionality:** Displays a triangle shape
