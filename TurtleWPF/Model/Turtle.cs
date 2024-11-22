public class Turtle
{
    private int x;
    private int y;
    private bool penDown;
    private string color;

    public Turtle()
    {
        x = 0;
        y = 0;
        penDown = true;
        color = "black";
    }

    public void Move(int value) => x += value;

    public void ChangeColor(string newColor) => color = newColor;

    public void PenDown() => penDown = true;

    public string GetState() => $"coords: ({x}, {y}), \n" +
                                $"penDown: {penDown}, \n" +
                                $"color: {color}";
}