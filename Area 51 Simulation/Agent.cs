namespace Area_51_Simulation
{
    class Agent
    {
        private string SecurityLevel;

        public Agent(string securityLevel)
        {
            SecurityLevel = securityLevel;
        }

        public string GetSecLvl()
        {
            return SecurityLevel;
        }
    }
}
