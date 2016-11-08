using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IP_Bin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        //Global Vars
        public static int[] undefined_ip = new int[4];
        public static int[] ip_a = new int[4];
        public static int[] ip_b = new int[4];
        public static int[] undefined_snm = new int[4];
        public static int[] sn_m = new int[4];
        public static int[] wild_card = new int[4];
        public static int glob_postfix;

        private void button1_Click(object sender, EventArgs e) {
            //Convert IP method
            //Check if !empty
            if (decip_okt0.Text.Equals("") || decip_okt1.Text.Equals("") || decip_okt2.Text.Equals("") || decip_okt3.Text.Equals("")) {
                MessageBox.Show("Please set correct values!");
                return;
            }

            //Read and convert
            int okt0_dec = Convert.ToInt32(decip_okt0.Text);
            String okt0_bin = tobin(okt0_dec);
            int okt1_dec = Convert.ToInt32(decip_okt1.Text);
            String okt1_bin = tobin(okt1_dec);
            int okt2_dec = Convert.ToInt32(decip_okt2.Text);
            String okt2_bin = tobin(okt2_dec);
            int okt3_dec = Convert.ToInt32(decip_okt3.Text);
            String okt3_bin = tobin(okt3_dec);

            //set to textbox
            ipbinbox.Text = okt0_bin + "." + okt1_bin + "." + okt2_bin + "." + okt3_bin;

            //set undefined_ip
            undefined_ip[0] = okt0_dec;
            undefined_ip[1] = okt1_dec;
            undefined_ip[2] = okt2_dec;
            undefined_ip[3] = okt3_dec;
        }

        private void addipa_Click(object sender, EventArgs e) {
            //Add to A
            ipabin.Text = ipbinbox.Text;
            //Define undefined_ip as ip_a
            ip_a = undefined_ip;
            //Set ip_a as ipa
            ipa.Text = "";
            for (int i = 0; i < 4; i++) { 
                ipa.Text = "" + ipa.Text + ip_a[i];
                if (i < 3) {
                    ipa.Text = ipa.Text + ".";
                }
            }
        }

        private void addipb_Click(object sender, EventArgs e) {
            //Add to B
            ipbbin.Text = ipbinbox.Text;
            //Define undefined_ip as ip_b
            ip_b = undefined_ip;
            //Set ip_a as ipb
            ipb.Text = "";
            for (int i = 0; i < 4; i++) {
                ipb.Text = "" + ipb.Text + ip_b[i];
                if (i < 3) {
                    ipb.Text = ipb.Text + ".";
                }
            }
        }

        public static String tobin(int dec_input) {
            //Check if Possible IP
            if (dec_input > 255) {
                return "ERROR";
            }
            
            //Convert to Binary
            String bin_ip = Convert.ToString(dec_input, 2);

            //Add Zeros
            String filling0s = "";
            if (bin_ip.Length < 8) {
                for (int i = 0; i < 8-bin_ip.Length; i++) {
                    filling0s = filling0s + "0";
                }
            }

            //Convert to Binary
            return filling0s + bin_ip;
        }

        private void translate_sn_Click(object sender, EventArgs e) {
            //Convert Subnetmask method

            //Check if !empty
            if (decnm_okt0.Text.Equals("") || decnm_okt1.Text.Equals("") || decnm_okt2.Text.Equals("") || decnm_okt3.Text.Equals("")) {
                MessageBox.Show("Please set correct values!");
                return;
            }

            //Read and convert
            int okt0_dec = Convert.ToInt32(decnm_okt0.Text);
            String okt0_bin = tobin(okt0_dec);
            int okt1_dec = Convert.ToInt32(decnm_okt1.Text);
            String okt1_bin = tobin(okt1_dec);
            int okt2_dec = Convert.ToInt32(decnm_okt2.Text);
            String okt2_bin = tobin(okt2_dec);
            int okt3_dec = Convert.ToInt32(decnm_okt3.Text);
            String okt3_bin = tobin(okt3_dec);

            //set undefined_snm
            undefined_snm[0] = okt0_dec;
            undefined_snm[1] = okt1_dec;
            undefined_snm[2] = okt2_dec;
            undefined_snm[3] = okt3_dec;

            //check if subnetmask is valid
            int[] tmp_snm = new int[4];
            tmp_snm[0] = Convert.ToInt32(okt0_bin);
            tmp_snm[1] = Convert.ToInt32(okt1_bin);
            tmp_snm[2] = Convert.ToInt32(okt2_bin);
            tmp_snm[3] = Convert.ToInt32(okt3_bin);
            glob_postfix = lint_mask(tmp_snm);

            //set to textbox
            snbinbox.Text = okt0_bin + "." + okt1_bin + "." + okt2_bin + "." + okt3_bin;
        }

        public static int lint_mask(int[] subnet_mask) {
            int postfix = 0;
            Boolean klackschalter = false;

            for (int i = 0; i < 4; i++) {
                //string parse = new String(Convert.ToString(subnet_mask[i]).ToArray());
                String parse = Convert.ToString(subnet_mask[i]);
                for (int x = 0; x < parse.Length; x++) {
                    //if (parse[x].Equals("1")) {
                    // MessageBox.Show("Ausgabe " + parse[x]);
                    if (parse[x] == '1') {
                        postfix++;
                        if (klackschalter == true && parse[x] == '1') {
                            MessageBox.Show("Invalid Subnetmask! @" + (x + 1) + " okt:" + (i + 1));
                            return 0;
                        }
                    } else {
                        klackschalter = true;
                    }

                }
            }
            return postfix;
        }

        private void addsnm_Click(object sender, EventArgs e) {
            //Set Postfix
            postfix_label.Text = "" + glob_postfix;

            snm.Text = decnm_okt0.Text + "." + decnm_okt1.Text + "." + decnm_okt2.Text + "." + decnm_okt3.Text;
            mbin.Text = snbinbox.Text;
            //Define undefined_snm as sn_m
            sn_m = undefined_snm;
            //Set Wildcard
            wild_card[0] = sn_m[3];
            wild_card[1] = sn_m[2];
            wild_card[2] = sn_m[1];
            wild_card[3] = sn_m[0];
            wildcard.Text = "";
            for (int i = 0; i < 4; i++)
            {
                wildcard.Text = wildcard.Text + wild_card[i];
                if (i < 3)
                {
                    wildcard.Text = wildcard.Text + ".";
                }
            }
        }

        private void check_conn_Click(object sender, EventArgs e) {
            //1&1 = 1 everything else = 0

            //create new Array with 4 Fields
            int[] chain = new int[4];
            //Get and from ip_a
            chain = and(ip_a,sn_m);
            //Set chain to chaina
            chaina.Text = tobin(chain[0]) + "." + tobin(chain[1]) + "." + tobin(chain[2]) + "." + tobin(chain[3]);
            //Get and from ip_b
            chain = and(ip_b, sn_m);
            //Set chain to chainb
            chainb.Text = tobin(chain[0]) + "." + tobin(chain[1]) + "." + tobin(chain[2]) + "." + tobin(chain[3]);
        }

        public static int[] and(int[] ip, int[] subnetmask) {
            int[] chain = new int[4];

            chain[0] = (ip[0] & subnetmask[0]);
            chain[1] = (ip[1] & subnetmask[1]);
            chain[2] = (ip[2] & subnetmask[2]);
            chain[3] = (ip[3] & subnetmask[3]);

            return chain;
        }

        private void classa_Click(object sender, EventArgs e) {
            decnm_okt0.Text = "255";
            decnm_okt1.Text = "0";
            decnm_okt2.Text = "0";
            decnm_okt3.Text = "0";
        }

        private void classb_Click(object sender, EventArgs e) {
            decnm_okt0.Text = "255";
            decnm_okt1.Text = "255";
            decnm_okt2.Text = "0";
            decnm_okt3.Text = "0";
        }

        private void classc_Click(object sender, EventArgs e) {
            decnm_okt0.Text = "255";
            decnm_okt1.Text = "255";
            decnm_okt2.Text = "255";
            decnm_okt3.Text = "0";
        }
    }
}
