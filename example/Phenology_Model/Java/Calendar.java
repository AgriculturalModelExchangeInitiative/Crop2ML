
import java.util.ArrayList;
import java.util.List;

public class JavaArrayOfArrayList {

	public static void main(String[] args) {
		List<String> l1 = new ArrayList<>();
		l1.add("1");
		l1.add("2");

		List<String> l2 = new ArrayList<>();
		l2.add("3");
		l2.add("4");
		l2.add("5");

		List<String>[] arrayOfList = new List[2];
		arrayOfList[0] = l1;
		arrayOfList[1] = l2;

		for (int i = 0; i < arrayOfList.length; i++) {
			List<String> l = arrayOfList[i];
			System.out.println(l);
		}

	}

}
