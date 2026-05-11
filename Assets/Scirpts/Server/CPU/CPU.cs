[System.Serializable]
public class CPU
{
    public ProcessorStatus status = null;

    public CPU(ProcessorStatus status)
    {
        this.status = status;
    }

    public float getTFLOPS()
    {
        if (status == null) return 0f;
        CPUPerformance performance = status.getPerformance().GetValue();
        return performance.cores * performance.max.ghz;
    }

    public float getMaxTFLOPS(Fan fan)
    {
        CPUPerformance performance = status.getPerformance().GetValue();
        float temp_fan = fan.Temperature_Decrement;
        if (performance.max.temperature < performance.MaxTemperature - temp_fan) return performance.cores * performance.max.ghz;
        else if (performance.medium.temperature < performance.MaxTemperature - temp_fan) return performance.cores * performance.medium.ghz;
        else return performance.cores * performance.min.ghz;
    }

    public ProcessorStatus getStatus() { return status; }

    public override string ToString()
    {
        return "";
    }
}