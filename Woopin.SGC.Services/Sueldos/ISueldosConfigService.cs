﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woopin.SGC.Common.HtmlModel;
using Woopin.SGC.Model.Common;
using Woopin.SGC.Model.Sueldos;

namespace Woopin.SGC.Services
{
    public interface ISueldosConfigService
    {
        #region Empleado
        void AddEmpleado(Empleado Empleado);
        void AddEmpleadoNT(Empleado Empleado);
        Empleado GetEmpleado(int Id);
        Empleado GetEmpleadoCompleto(int Id);
        void UpdateEmpleado(Empleado Empleado);
        IList<Empleado> GetAllEmpleados();
        int GetProximoNumeroReferencia();

        void DeleteEmpleados(List<int> Ids);
        SelectCombo GetAllEmpleadosByFilterCombo(SelectComboRequest req);
        SelectCombo GetEmpleadoCombos();
        bool ExistCUITNT(string cuit, int? IdUpdate);
        void ImportEmpleado(Empleado c);
        void ImportEmpleadoNT(Empleado c);
        void ImportEmpleados(List<Empleado> Empleados);
        #endregion

        #region Adicional
        void AddAdicional(Adicional Adicional);
        void AddAdicionalNT(Adicional Adicional);
        Adicional GetAdicional(int Id, int IdSindicato, bool OnlyManual);
        void UpdateAdicional(Adicional Adicional, IList<AdicionalAdicionales> Adicionales = null);
        IList<Adicional> GetAllAdicionales();
        void DeleteAdicionales(List<int> Ids);
        SelectCombo GetAllAdicionalesByFilterCombo(SelectComboRequest req, int IdSindicato, bool OnlyManual);
        SelectCombo GetAdicionalCombos();
        void AddAdicionalConAdicionales(Adicional Adicional, IList<Adicional> Adicionales);
        #endregion

        #region AdicionalRecibo
        void AddAdicionalRecibo(AdicionalRecibo AdicionalRecibo);
        void AddAdicionalReciboNT(AdicionalRecibo AdicionalRecibo);
        AdicionalRecibo GetAdicionalRecibo(int Id);
        AdicionalRecibo GetAdicionalReciboNT(int Id);
        //void UpdateAdicionalRecibo(AdicionalRecibo AdicionalRecibo, IList<AdicionalAdicionales> Adicionales = null);
        //IList<AdicionalRecibo> GetAllAdicionalReciboes();
        void DeleteAdicionalRecibos(List<int> Ids);
        //SelectCombo GetAllAdicionalReciboesByFilterCombo(SelectComboRequest req);
        //SelectCombo GetAdicionalReciboCombos();
        //void AddAdicionalReciboConAdicionalReciboes(AdicionalRecibo AdicionalRecibo, IList<AdicionalRecibo> AdicionalReciboes);
        IList<AdicionalRecibo> GetAdicionalesDelPeriodoByEmpleado(string Periodo, int IdEmpleado);
        
        #endregion



        #region AdicionalAdicionales
        void AddAdicionalAdicionales(AdicionalAdicionales AdicionalAdicionales);
        void AddAdicionalAdicionalesNT(AdicionalAdicionales AdicionalAdicionales);
        IList<AdicionalAdicionales> GetAllAdicionalAdicionaleses();
        AdicionalAdicionales GetAdicionalAdicionales(int Id);
        IList<AdicionalAdicionales> GetAdicionalAdicionalesByAdicional(int Id);
        //void UpdateAdicionalAdicionales(AdicionalAdicionales AdicionalAdicionales);
        //void DeleteAdicionalAdicionaleses(List<int> Ids);
        //SelectCombo GetAllAdicionalAdicionalesesByFilterCombo(SelectComboRequest req);
        //SelectCombo GetAdicionalAdicionalesCombos();
        #endregion



    }
}
