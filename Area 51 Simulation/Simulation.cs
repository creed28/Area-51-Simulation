namespace Area_51_Simulation
{
    static class Simulation
    {
        private static bool _stopSimulation = false;
        private static Random _random = new();
        private static string[] _securityLevels = { "Confidential", "Secret", "Top-Secret" };
        private static Elevator _elevator = new();
        private static string[] _elevatorFloors = { "G", "S", "T1", "T2" };

        public static void Run()
        {
            //Симулацията ще приключи след натискане на бутона Enter и се изпълни текущата операция
            new Thread(UserInputThread).Start();

            while (!_stopSimulation)
            {
                int index = _random.Next(0, 3);
                Agent agent = new(_securityLevels[index]);
                string floor = _elevatorFloors[_random.Next(1, 4)];

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("An agent with security level " + agent.GetSecLvl() + " has entered The Base!");

                Console.ForegroundColor = ConsoleColor.Yellow;

                if (_elevator.GetCurrentFloor() != "G")
                {
                    _elevator.ChangeFloor("G");
                    Console.WriteLine("Elevator is not on this floor, call it!");

                    if (ElevatorWaitTime(floor) == 0)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1000);
                        Console.WriteLine("Agent pressed the button to call the elevator");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        /* Още един вариант
                           Thread.Sleep(1000 * ElevatorWaitTime(floor));
                        Console.WriteLine("Elevator moved " + ElevatorWaitTime(floor) + " floors from " + floor + " to floor " + _elevator.GetCurrentFloor()); */
                        Console.WriteLine("Elevator is coming from floor " + floor + " to floor " + _elevator.GetCurrentFloor());
                        for (int i = 1; i <= ElevatorWaitTime(floor); i++)
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Elevator moved " + i + " floors");
                        }
                        Thread.Sleep(1000);
                        Console.WriteLine("Elevator arrived on floor " + _elevator.GetCurrentFloor());
                    }
                }

                while (floor != "G" || _elevator.GetCurrentFloor() != "G")
                {
                    if (_stopSimulation)
                    {
                        break;
                    }

                    GetAgentState(floor, agent);

                    _elevator.ChangeFloor(floor);

                    while (!CheckAccess(agent, floor))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Agent " + agent.GetSecLvl() + " doesn't have the security level to access floor " + floor);
                        floor = _elevatorFloors[_random.Next(0, 4)];

                        if (_stopSimulation)
                        {
                            break;
                        }

                        GetAgentState(floor, agent);

                        _elevator.ChangeFloor(floor);

                        if (floor == "G" || _stopSimulation)
                        {
                            break;
                        }
                    }

                    if (floor == "G" || _stopSimulation)
                    {
                        break;
                    }

                    if (agent.GetSecLvl() == "Top-Secret")
                    {
                        floor = _elevator.GetCurrentFloor();
                    }
                    else
                    {
                        _elevator.GoToRandomFloor();
                    }

                    if (_elevator.GetCurrentFloor() != floor)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Elevator is not on this floor, call it!");

                        if (ElevatorWaitTime(floor) == 0)
                        {
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1000);
                            Console.WriteLine("Agent pressed the button to call the elevator");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            /* Още един вариант
                               Thread.Sleep(1000 * ElevatorWaitTime(floor));
                             Console.WriteLine("Elevator moved " + ElevatorWaitTime(floor) + " floors from " + _elevator.GetCurrentFloor() + " to floor " + floor); */
                            Console.WriteLine("Elevator is coming from floor " + _elevator.GetCurrentFloor() + " to floor " + floor);
                            for (int i = 1; i <= ElevatorWaitTime(floor); i++)
                            {
                                Thread.Sleep(1000);
                                Console.WriteLine("Elevator moved " + i + " floors");
                            }
                            Thread.Sleep(1000);
                            Console.WriteLine("Elevator arrived on floor " + floor);
                        }

                        _elevator.ChangeFloor(floor);

                    }

                    floor = _elevatorFloors[_random.Next(0, 4)];

                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Agent left The Base!");
                Thread.Sleep(1000);
                Console.WriteLine();
            }

            Console.WriteLine("Simulation stopped!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void UserInputThread()
        {
            while (!_stopSimulation)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        _stopSimulation = true;
                    }
                }
            }
        }

        public static void GetAgentState(string floor, Agent agent)
        {
            if (ElevatorWaitTime(floor) == 0)
            {
                Thread.Sleep(1000);
            }
            else
            {
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Agent pressed the button to go to floor " + floor);
                for (int i = 1; i <= ElevatorWaitTime(floor); i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Agent moved " + i + " floors");
                }
                Thread.Sleep(1000);
                Console.WriteLine("Agent arrived on floor " + floor);
                Thread.Sleep(1000);

                if (CheckAccess(agent, floor))
                {
                    if (floor == "G")
                    {
                        Console.WriteLine("Agent is finishing his job before he leaves");
                    }
                    else
                    {
                        Console.WriteLine("Agent is doing his job");
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        //Изчислява колко етажа ще се предвижива асансьорът, като за всеки етаж отнема по 1 секунда
        public static int ElevatorWaitTime(string floor)
        {
            int elevatorFloor = _elevatorFloors.ToList().FindIndex(s => s == _elevator.GetCurrentFloor()) + 1;
            int calledFloor = _elevatorFloors.ToList().FindIndex(s => s == floor) + 1;
            return Math.Abs(elevatorFloor - calledFloor);
        }

        public static bool CheckAccess(Agent agent, string floor)
        {
            if (agent.GetSecLvl() == "Confidential" && floor != "G")
            {
                return false;
            }
            else if (agent.GetSecLvl() == "Secret" && floor != "G" && floor != "S")
            {
                return false;
            }

            return true;
        }
    }
}
