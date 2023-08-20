
using ENTITIES;
using MAXIUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MAXIUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewEmployees.Visibility = Visibility.Visible;
            ViewBeneficiaries.Visibility = Visibility.Collapsed;
            dpkDateOfBirth.DisplayDateEnd = DateTime.Now.AddYears(-18);
            dpkDateOfBirthB.DisplayDateEnd = DateTime.Now.AddYears(-18);

        }

        private void btnLoadEmployees_Click(object sender, RoutedEventArgs e)
        {
            GetEmployees();
        }

        private async void GetEmployees()
        {
            var employees = await new EmployeeRepository().GetEmployeesAsync();
            dgEmployees.DataContext = employees;
            if (employees.Count == 0)
                MessageBox.Show("There aren't employees");
        }

        private void btnEditEmployee(object sender, RoutedEventArgs e)
        {
            Employee? emp = ((FrameworkElement)sender).DataContext as Employee;
            txtCurp.Text = emp?.Curp;
            txtEmployeeNumber.Text = emp?.EmployeeNumber.ToString();
            txtFirstName.Text = emp?.FirstName;
            txtLastName.Text = emp?.LastName;
            txtId.Text = emp?.EmployeeId.ToString();
            txtNationality.Text = emp?.Nationality;
            txtNss.Text = emp?.Ssn;
            txtPhone.Text = emp?.PhoneNumber;
            txtIsDeleted.Text = emp?.IsDeleted.ToString();
            dpkDateOfBirth.SelectedDate = new DateTime(emp.DateOfBirth.Year, emp.DateOfBirth.Month, emp.DateOfBirth.Day);
            btnCancelEdit.Visibility = Visibility.Visible;
            btnSaveEmployee.Content = "Update Employee";
        }

        private void btnDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Employee? emp = ((FrameworkElement)sender).DataContext as Employee;
            DeleteEmployee(emp.EmployeeId);
            ViewBeneficiaries.Visibility = Visibility.Collapsed;
            btnSaveEmployee.Content = "Save Employee";
        }

        private async void DeleteEmployee(int Id)
        {
            var employees = await new EmployeeRepository().DeleteEmployeeAsync(Id);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employees.Message);

        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            btnSaveEmployee.Content = "Save Employee";
            btnCancelEdit.Visibility = Visibility.Hidden;
        }

        private void ClearData()
        {
            txtCurp.Text = string.Empty;
            txtEmployeeNumber.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtNationality.Text = string.Empty;
            txtNss.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtIsDeleted.Text = string.Empty;
            dpkDateOfBirth.SelectedDate = null;
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeIsValid())
            {
                Employee emp = new Employee
                {
                    Curp = txtCurp.Text,
                    EmployeeNumber = Convert.ToInt32(txtEmployeeNumber.Text),
                    FirstName = txtFirstName.Text,
                    DateOfBirth = dpkDateOfBirth.SelectedDate.Value,
                    LastName = txtLastName.Text,
                    Nationality = txtNationality.Text,
                    PhoneNumber = txtPhone.Text,
                    Ssn = txtNss.Text
                };
                if (btnCancelEdit.Visibility == Visibility.Visible)
                {
                    emp.EmployeeId = Convert.ToInt32(txtId.Text);
                    UpdateEmp(emp);
                }
                else
                {
                    AddEmp(emp);
                }
            }
        }

        private async void UpdateEmp(Employee emp)
        {
            var employeeUpdated = await new EmployeeRepository().UpdateEmployeeAsync(emp);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employeeUpdated.Message);
        }

        private async void AddEmp(Employee emp)
        {
            var employeeUpdated = await new EmployeeRepository().AddEmployeeAsync(emp);
            GetEmployees();
            ClearData();
            btnCancelEdit.Visibility = Visibility.Hidden;
            MessageBox.Show(employeeUpdated.Message);
        }

        private bool EmployeeIsValid()
        {
            bool valid = false;
            if (txtCurp.Text != string.Empty &&
                txtFirstName.Text != string.Empty &&
                txtEmployeeNumber.Text != string.Empty &&
                txtLastName.Text != string.Empty &&
                txtNationality.Text != string.Empty &&
                txtNss.Text != string.Empty &&
                txtPhone.Text != string.Empty &&
                dpkDateOfBirth.SelectedDate != null)
            {
                valid = true;
            }
            else
            {
                MessageBox.Show("Type the Employee missing information");
            }
            return valid;
        }

        private void btnAdminBeneficiariesByEmployee(object sender, RoutedEventArgs e)
        {
            Employee? emp = ((FrameworkElement)sender).DataContext as Employee;
            //ViewEmployees.Visibility = Visibility.Collapsed;
            ViewBeneficiaries.Visibility = Visibility.Visible;
            GetBeneficiaries(emp.EmployeeId);
            txtEmployeeIdB.Text = emp.EmployeeId.ToString();
            lblLegendEmployee.Content = "A" + emp.FirstName + " " + emp.LastName;
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            ViewEmployees.Visibility = Visibility.Visible;
            ViewBeneficiaries.Visibility = Visibility.Collapsed;
        }

        private async void GetBeneficiaries(int Id)
        {
            var beneficiaries = await new BeneficiaryRepository().GetBeneficiariesAsync(Id);
            dgBeneficiaries.DataContext = beneficiaries;
             

        }

        private void btnEditBeneficiary(object sender, RoutedEventArgs e)
        {
            Beneficiary? ben = ((FrameworkElement)sender).DataContext as Beneficiary;
            txtCurpB.Text = ben?.Curp;
            txtEmployeeIdB.Text = ben?.EmployeeId.ToString();
            txtFirstNameB.Text = ben?.FirstName;
            txtLastNameB.Text = ben?.LastName;
            txtIdB.Text = ben?.BeneficiaryId.ToString();
            txtParticipationPercentageOrigin.Text = ben?.ParticipationPercentage.ToString();
            txtParticipationPercentage.Text = ben?.ParticipationPercentage.ToString();
            txtNationalityB.Text = ben?.Nationality;
            txtNssB.Text = ben?.Ssn;
            txtPhoneB.Text = ben?.PhoneNumber;
            txtIsDeletedB.Text = ben?.IsDeleted.ToString();
            dpkDateOfBirthB.SelectedDate = new DateTime(ben.DateOfBirth.Year, ben.DateOfBirth.Month, ben.DateOfBirth.Day);
            btnCancelEditB.Visibility = Visibility.Visible;
            btnSaveBeneficiary.Content = "Update Beneficiary";
        }

        private void btnCancelEditB_Click(object sender, RoutedEventArgs e)
        {
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
            btnSaveBeneficiary.Content = "Save Beneficiary";
        }

        private void ClearDataB()
        {
            txtCurpB.Text = string.Empty;
            txtFirstNameB.Text = string.Empty;
            txtLastNameB.Text = string.Empty;
            txtIdB.Text = string.Empty;
            txtNationalityB.Text = string.Empty;
            txtNssB.Text = string.Empty;
            txtPhoneB.Text = string.Empty;
            txtIsDeletedB.Text = string.Empty;
            txtParticipationPercentage.Text = String.Empty;
            dpkDateOfBirthB.SelectedDate = null;
        }

        private void btnDeleteBeneficiary(object sender, RoutedEventArgs e)
        {
            Beneficiary? ben = ((FrameworkElement)sender).DataContext as Beneficiary;
            DeleteBeneficiary(ben.BeneficiaryId, ben.EmployeeId);
            btnSaveBeneficiary.Content = "Save Beneficiary";
        }

        private async void DeleteBeneficiary(int Id, int EmployeeId)
        {
            var ben = await new BeneficiaryRepository().DeleteBeneficiaryAsync(Id);
            GetBeneficiaries(EmployeeId);
            ClearData();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(ben.Message);

        }

        private bool BeneficiaryIsValid()
        {
            bool valid = false;
            if (txtCurpB.Text != string.Empty &&
                txtFirstNameB.Text != string.Empty &&
                txtEmployeeIdB.Text != string.Empty &&
                txtLastNameB.Text != string.Empty &&
                txtNationalityB.Text != string.Empty &&
                txtNssB.Text != string.Empty &&
                txtPhoneB.Text != string.Empty &&
                dpkDateOfBirthB.SelectedDate != null &&
                txtParticipationPercentage.Text != null)
            {
                valid = true;
            }
            else
            {
                MessageBox.Show("Type the Beneficiary missing information");
            }
            return valid;
        }

        private async void btnSaveBeneficiary_Click(object sender, RoutedEventArgs e)
        {
            if (BeneficiaryIsValid())
            {

              
                if (Convert.ToInt32(txtParticipationPercentage.Text) > 100)
                {
                    MessageBox.Show("The percentages cannot be greater than 100");
                    return;
                }
                List<Beneficiary> SumBeneficiaries = await new BeneficiaryRepository().GetSumPercentageBeneficiariesAsync(Convert.ToInt32(txtEmployeeIdB.Text));

         
                Beneficiary ben = new Beneficiary
                {
                    Curp = txtCurpB.Text,
                    EmployeeId = Convert.ToInt32(txtEmployeeIdB.Text),
                    FirstName = txtFirstNameB.Text,
                    DateOfBirth = dpkDateOfBirthB.SelectedDate.Value,
                    LastName = txtLastNameB.Text,
                    Nationality = txtNationalityB.Text,
                    PhoneNumber = txtPhoneB.Text,
                    Ssn = txtNssB.Text,
                    ParticipationPercentage = Convert.ToInt32(txtParticipationPercentage.Text)
                };

                if (btnCancelEditB.Visibility == Visibility.Visible)
                {
                    if (SumBeneficiaries.Any() && (SumBeneficiaries[0].ParticipationPercentage - Convert.ToInt32(txtParticipationPercentageOrigin.Text)) + Convert.ToInt32(txtParticipationPercentage.Text) > 100)
                    {
                        MessageBox.Show("The sum of the percentages cannot be greater than 100");
                        return;
                    }

                    ben.BeneficiaryId = Convert.ToInt32(txtIdB.Text);
                    UpdateBen(ben);
                }
                else
                {
         

                    if (SumBeneficiaries.Any() && SumBeneficiaries[0].ParticipationPercentage + Convert.ToInt32(txtParticipationPercentage.Text) > 100)
                    {
                        MessageBox.Show("The sum of the percentages cannot be greater than 100");
                        return;
                    }

                    AddBen(ben);
                }
                btnSaveBeneficiary.Content = "Save Beneficiary";
            }
        }

        private async void UpdateBen(Beneficiary ben)
        {
            var benUpdated = await new BeneficiaryRepository().UpdateBeneficiaryAsync(ben);
            GetBeneficiaries(ben.EmployeeId);
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(benUpdated.Message);
        }

        private async void AddBen(Beneficiary ben)
        {
            var benUpdated = await new BeneficiaryRepository().AddBeneficiaryAsync(ben);
            GetBeneficiaries(ben.EmployeeId);
            ClearDataB();
            btnCancelEditB.Visibility = Visibility.Hidden;
            MessageBox.Show(benUpdated.Message);
        }

        private void txtEmployeeNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtEmployeeNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

            txtEmployeeNumber.Text = txtEmployeeNumber.Text.Trim();
            if (txtEmployeeNumber.Text.Trim().Length > 0 && txtEmployeeNumber.Text.Trim()[0] == '0')
                txtEmployeeNumber.Text = txtEmployeeNumber.Text.Trim().Substring(1).Trim();
        }

        private void txtParticipationPercentage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void txtParticipationPercentage_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtParticipationPercentage.Text = txtParticipationPercentage.Text.Trim();
            if (txtParticipationPercentage.Text.Trim().Length > 0 && txtParticipationPercentage.Text.Trim()[0] == '0')
                txtParticipationPercentage.Text = txtParticipationPercentage.Text.Trim().Substring(1).Trim();
        }
    }
}
