public class CreateRadioButtonsExample extends Applet{

    public void init(){

        /*
         * To create a checkbox group, use
         * CheckboxGroup()
         * constructor of AWT CheckboxGroup class.
         */

        CheckboxGroup lngGrp = new CheckboxGroup();

        //if you create checkboxes and add to group,they become radio buttons
        Checkbox java = new Checkbox("Java", lngGrp, true);
        Checkbox cpp = new Checkbox("C++", lngGrp, true);
        Checkbox vb = new Checkbox("VB", lngGrp, true);

        //add radio buttons
        add(java);
        add(cpp);
        add(vb);
    }
}