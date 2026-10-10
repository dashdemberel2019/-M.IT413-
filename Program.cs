using Avalonia;                                   // Avalonia-ийн үндсэн классууд (AppBuilder, Application г.м.)
using Avalonia.Controls;                          // Window, Button, TextBox, Canvas зэрэг виджетүүд
using Avalonia.Controls.ApplicationLifetimes;     // Desktop аппын амьдралын мөчлөг (MainWindow тохируулах)
using Avalonia.Interactivity;                     // RoutedEventArgs — товч дарах зэрэг event-ийн мэдээлэл
using Avalonia.Themes.Fluent;                     // FluentTheme — виджетүүдийн загвар (харагдах байдал)
using Avalonia.Media;                             // Brushes, FontWeight — өнгө, фонт зэрэг зураглалын хэрэгсэл


class Program                                      // Програмын эхлэлийн класс
{
    static void Main(string[] args)                // Аппын гарааны цэг (энэ метод хамгийн түрүүнд ажиллана)
        => AppBuilder.Configure<App>()             // App классыг ашиглан Avalonia апп бүтээх
            .UsePlatformDetect()                   // Ажиллаж буй платформыг (Linux/Windows/macOS) автоматаар илрүүлэх
            .StartWithClassicDesktopLifetime(args);// Desktop горимд аппыг эхлүүлэх
}

class App : Application                             // Аппын үндсэн класс (Application-аас удамшсан)
{
    public override void Initialize() => Styles.Add(new FluentTheme()); // Fluent загварыг апп даяар нэмэх (энгүй бол TextBox үл үзэгдэнэ)

    public override void OnFrameworkInitializationCompleted() // Framework бэлэн болсны дараа дуудагдана
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) // Desktop горим мөн эсэхийг шалгах
            desktop.MainWindow = new MainWindow(); // Үндсэн цонхыг үүсгэж тохируулах
        base.OnFrameworkInitializationCompleted(); // Эх классын анхны үйлдлийг мөн дуудах
    }
}

class MainWindow : Window                           // Үндсэн цонхны класс (Window-оос удамшсан)
{
    private TextBox textBox1;                       // Эхний тоо оруулах талбар (private = зөвхөн энэ класс дотор хандана)
    private TextBox textBox2;                       // Хоёр дахь тоо оруулах талбар
    private TextBlock resultLabel;                  // Үр дүн харуулах шошго

    public MainWindow()                             // Цонхны байгуулагч (constructor) — цонх үүсэх үед ажиллана
    {
        Title = "Хоёр тоо нэмэх хялбар програм";    // Цонхны гарчиг (дээд мөрөнд харагдана)
        Width = 400;                                // Цонхны өргөн (пиксел)
        Height = 300;                               // Цонхны өндөр (пиксел)

        var canvas = new Canvas();                  // Canvas — виджетүүдийг пикселээр (x, y) байрлуулах хавтас

        // Эхний талбар: цагаан дэвсгэр, цэнхэр өнгө, 16 хэмжээ, тод (bold) фонт
        textBox1 = new TextBox { Width = 100, Text = "12", Background = Brushes.White, Foreground = Brushes.Blue, FontSize = 16, FontWeight = FontWeight.Bold };
        Canvas.SetLeft(textBox1, 50); Canvas.SetTop(textBox1, 30);   // textBox1-ийг зүүнээс 50, дээрээс 30 пикселд байрлуулах

        textBox2 = new TextBox { Width = 100, Text = "13" };         // Хоёр дахь талбар, анхны утга "13"
        Canvas.SetLeft(textBox2, 50); Canvas.SetTop(textBox2, 70);   // textBox2-ийг (50, 70)-д байрлуулах

        var button = new Button { Content = "+", Width = 30 };       // "+" (нэмэх) товч, өргөн 30
        Canvas.SetLeft(button, 50); Canvas.SetTop(button, 110);      // (50, 110)-д байрлуулах
        button.Click += Button_Click;                               // Дарахад Button_Click метод дуудагдана

        var button1 = new Button { Content = "-", Width = 30 };      // "-" (хасах) товч
        Canvas.SetLeft(button1, 90); Canvas.SetTop(button1, 110);    // (90, 110)-д, "+" товчны хажууд
        button1.Click += Button1_Click;                             // Дарахад Button1_Click метод дуудагдана

        // "Гарах" товч: тод фонт, цагаан дэвсгэр, цэнхэр хүрээ ба текст
        var clse = new Button { Content = "Гарах уу", Width = 100, FontSize = 16, FontWeight = FontWeight.Bold, Background = Brushes.White, BorderBrush = Brushes.Blue, Foreground = Brushes.Blue };
        Canvas.SetLeft(clse, 50); Canvas.SetTop(clse, 200);         // (50, 200)-д байрлуулах
        clse.Click += (s, e) => Close();                           // Дарахад цонхыг хаах (lambda илэрхийлэл)

        resultLabel = new TextBlock { Width = 250, Text = "Үр  дүн : " }; // Үр дүнгийн шошго, анхны текст
        Canvas.SetLeft(resultLabel, 50); Canvas.SetTop(resultLabel, 160); // (50, 160)-д байрлуулах

        canvas.Children.Add(textBox1);              // textBox1-ийг canvas-д нэмэх
        canvas.Children.Add(textBox2);              
        canvas.Children.Add(button);                
        canvas.Children.Add(button1);               
        canvas.Children.Add(clse);                  
        canvas.Children.Add(resultLabel);           

        Content = canvas;                           // Canvas-ийг цонхны агуулга болгож тохируулах
    }

    private void Button1_Click(object sender, RoutedEventArgs e) // "-" товч дарахад ажиллах метод
    {
        int a = int.Parse(textBox1.Text);           
        int b = int.Parse(textBox2.Text);           
        int sum = a - b;                            
        resultLabel.Text = sum.ToString();          
    }

    private void Button_Click(object sender, RoutedEventArgs e)  // "+" товч дарахад ажиллах метод
    {
        int a = int.Parse(textBox1.Text);           
        int b = int.Parse(textBox2.Text);           
        int sum = a + b;                            
        resultLabel.Text = sum.ToString();          
    }
}