import { test, expect } from '@playwright/test';

test('test', async ({ page }) => {
    await page.goto('https://bpcalculator.azurewebsites.net/');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('89');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('59');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#form1')).toContainText('Low Blood Pressure');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('119');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('79');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#form1')).toContainText('Ideal Blood Pressure');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('139');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('89');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#form1')).toContainText('Pre-High Blood Pressure');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('150');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('95');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#form1')).toContainText('High Blood Pressure');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('12');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('13');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#BP_Systolic-error')).toContainText('Invalid Systolic Value');
    await expect(page.locator('#BP_Diastolic-error')).toContainText('Invalid Diastolic Value');
    await page.getByRole('spinbutton', { name: 'Systolic' }).fill('');
    await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('');
    await page.getByRole('button', { name: 'Submit' }).click();
    await expect(page.locator('#BP_Systolic-error')).toContainText('The Systolic field is required.');
    await expect(page.locator('#BP_Diastolic-error')).toContainText('The Diastolic field is required.');
    await expect(async () => {
        await page.getByRole('spinbutton', { name: 'Systolic' }).fill('abc');
    }).rejects.toThrowError(/Cannot type text into input\[type=number\]/);

    await expect(async () => {
        await page.getByRole('spinbutton', { name: 'Diastolic' }).fill('!@#');
    }).rejects.toThrowError(/Cannot type text into input\[type=number\]/);
});