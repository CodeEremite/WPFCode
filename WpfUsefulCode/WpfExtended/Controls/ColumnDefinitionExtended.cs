﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfExtended
{
    public class ColumnDefinitionExtended:ColumnDefinition
    {
        public static DependencyProperty    VisibleProperty;
        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty);}
            set { SetValue(VisibleProperty, value); }
        }
        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }

        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }
            
        static ColumnDefinitionExtended()
        {
            VisibleProperty = DependencyProperty.Register(
                                        "Visible", 
                                        typeof(Boolean), 
                                        typeof(ColumnDefinitionExtended),
                                        new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged))
                                        );

            ColumnDefinition.WidthProperty.OverrideMetadata(
                typeof(ColumnDefinitionExtended),
                new FrameworkPropertyMetadata(new GridLength(1,GridUnitType.Star), null, new CoerceValueCallback(CoerceWidth))
                );

            ColumnDefinition.MinWidthProperty.OverrideMetadata(
                typeof(ColumnDefinitionExtended),
                new FrameworkPropertyMetadata((Double)0, null, new CoerceValueCallback(CoerceMinWidth))
                );
        }
        
        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(ColumnDefinition.WidthProperty);
            obj.CoerceValue(ColumnDefinition.MinWidthProperty);
        }

        static Object CoerceWidth(DependencyObject obj, Object nValue)
        {
            return (((ColumnDefinitionExtended)obj).Visible)? nValue : new GridLength(0);
        }
        static Object CoerceMinWidth(DependencyObject obj, Object nValue)
        {
            return (((ColumnDefinitionExtended)obj).Visible)? nValue : (Double)0;
        }
    }
}
