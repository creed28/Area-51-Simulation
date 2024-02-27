namespace Area_51_Simulation
{
    class Elevator
    {
        string[] ElevatorFloors = { "G", "S", "T1", "T2" };
        string CurrentFloor;
        Random random = new();

        public Elevator()
        {
            CurrentFloor = ElevatorFloors[random.Next(0, 4)];
        }

        public void ChangeFloor(string floor)
        {
            if (CurrentFloor == floor)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Agent pressed the button to go to floor " + floor);
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You are already on this floor, agent!");
                return;
            }

            CurrentFloor = ElevatorFloors.Where(s => s == floor).Single();

        }

        public string GetCurrentFloor()
        {
            return CurrentFloor;
        }

        public void GoToRandomFloor()
        {
            CurrentFloor = ElevatorFloors[random.Next(0, 4)];
        }

    }
}
